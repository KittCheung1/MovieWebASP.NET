using AutoMapper;
using TestWebASP.NET.DTO.Requests;
using TestWebASP.NET.DTO.Responses;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, ReadMovieDTO>()
                .ReverseMap();
            CreateMap<Movie, CreateMovieDTO>()
                .ReverseMap();
        }
    }
}
