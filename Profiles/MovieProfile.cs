using AutoMapper;
using MovieWebASP.NET.DTO.Requests;
using MovieWebASP.NET.DTO.Responses;
using MovieWebASP.NET.Models;

namespace MovieWebASP.NET.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, ReadMovieDTO>()
                .ReverseMap();
            CreateMap<Movie, CreateMovieDTO>()
                .ReverseMap();
            CreateMap<Movie, UpdateMovieDTO>()
                .ReverseMap();
        }
    }
}
