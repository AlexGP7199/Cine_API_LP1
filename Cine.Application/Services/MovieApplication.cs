using AutoMapper;
using Cine.Application.Commons.Bases;
using Cine.Application.Dtos.Request.Movie;
using Cine.Application.Dtos.Response.Movie;
using Cine.Application.Interfaces;
using Cine.Application.Validators.Movies;
using Cine.Domain.Entities;
using Cine.Infrastructure.Commons.Bases.Request;
using Cine.Infrastructure.Commons.Bases.Response;
using Cine.Infrastructure.Persistence.Interfaces;
using Cine.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Application.Services
{
    public class MovieApplication : IMovieApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MovieValidator _movieValidator;

        public MovieApplication(IUnitOfWork unitOfWork, IMapper mapper, MovieValidator movieValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _movieValidator = movieValidator;
        }

        public async Task<BaseResponse<BaseEntityResponse<MovieResponseDto>>> ListMovies(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<MovieResponseDto>>();
            var movies = await _unitOfWork.movie.ListMovies(filters);

            if(movies != null) 
            { 
                response.isSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<MovieResponseDto>>(movies);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.isSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<MovieResponseDto>> GetMovieById(int id)
        {
            var response = new BaseResponse<MovieResponseDto>();
            var movies = await _unitOfWork.movie.GetByIdAsync(id);

            if( movies != null )
            {
                response.isSuccess = true;
                response.Data = _mapper.Map<MovieResponseDto>(movies);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.isSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RegisterMovie(MovieRequestDto movieDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _movieValidator.ValidateAsync(movieDto);
            if( !validationResult.IsValid )
            {
                response.isSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }

            var movie = _mapper.Map<Pelicula>(movieDto);
            response.Data = await _unitOfWork.movie.RegisterAsync(movie);

            if(response.Data == true )
            {
                response.isSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
                return response;
            }
            else
            {
                response.isSuccess =false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> UpdateMovie(int movieId, MovieUpdateRequestDto movieDto)
        {
            var response = new BaseResponse<bool>();
            var movieEdit = await GetMovieById(movieId);
            if (movieEdit.Data is null)
            {
                response.isSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var movie = _mapper.Map<Pelicula>(movieDto);
            movie.Id = movieId;
            response.Data = await _unitOfWork.movie.EditAsync(movie);

            if (response.Data == true)
            {
                response.isSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.isSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveMovie(int movieId)
        {
            var response = new BaseResponse<bool>();
            var movie = await GetMovieById(movieId);

            if(movie.Data is null) 
            {
                response.isSuccess =false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            
            response.Data = await _unitOfWork.movie.RemoveAsync(movieId);

            if (response.Data)
            {
                response.isSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.isSuccess = false;
                response.Message= ReplyMessage.MESSAGE_FAILED;
            }

            return response;

        }

      
    }
}
