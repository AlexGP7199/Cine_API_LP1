using AutoMapper;
using Cine.Application.Dtos.Request.Movie;
using Cine.Application.Dtos.Response.Movie;
using Cine.Domain.Entities;
using Cine.Infrastructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine.Application.Mappers.MovieMappers
{
    public class MovieMapperProfile : Profile
    {
        public MovieMapperProfile()
        {
            CreateMap<MovieRequestDto, Pelicula>().ReverseMap();
            CreateMap<MovieUpdateRequestDto, Pelicula>().ReverseMap();
            CreateMap<Pelicula, MovieResponseDto>().ReverseMap();
            CreateMap<BaseEntityResponse<MovieResponseDto>, BaseEntityResponse<Pelicula>>().ReverseMap();
        }
    }
}
