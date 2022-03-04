using AutoMapper;
using MovieWebASP.NET.DTO.Requests;
using MovieWebASP.NET.DTO.Responses;
using MovieWebASP.NET.Models;

namespace MovieWebASP.NET.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {

            CreateMap<Character, ReadCharacterDTO>()
                .ReverseMap();

            CreateMap<Character, CreateCharacterDTO>()
                .ReverseMap();

            CreateMap<Character, UpdateCharacterDTO>()
                .ReverseMap();

        }
    }
}
