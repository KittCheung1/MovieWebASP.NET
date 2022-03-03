using AutoMapper;
using TestWebASP.NET.DTO.Requests;
using TestWebASP.NET.DTO.Responses;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {

            CreateMap<Character, ReadCharacterDTO>()
                .ReverseMap();

            CreateMap<Character, CreateCharacterDTO>()
                .ReverseMap();

            //CreateMap<Character, UpdateCharacterDTO>()
            //    .ForMember(characterReadDTO => characterReadDTO.Movie, opt => opt
            //    .MapFrom(c => c.Movies))
            //    .ReverseMap();
        }
    }
}
