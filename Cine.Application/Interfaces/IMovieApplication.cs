using Cine.Application.Commons.Bases;
using Cine.Application.Dtos.Request.Movie;
using Cine.Application.Dtos.Response.Movie;
using Cine.Infrastructure.Commons.Bases.Request;
using Cine.Infrastructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Application.Interfaces
{
    public interface IMovieApplication
    {
        Task<BaseResponse<BaseEntityResponse<MovieResponseDto>>> ListMovies(BaseFiltersRequest filters);
        //Task<BaseResponse<>>
        Task<BaseResponse<MovieResponseDto>> GetMovieById(int id);
        Task<BaseResponse<bool>> RegisterMovie (MovieRequestDto movie);
        Task<BaseResponse<bool>> UpdateMovie(int movieId, MovieUpdateRequestDto movie);
        Task<BaseResponse<bool>> RemoveMovie(int movieId);
    }
}
