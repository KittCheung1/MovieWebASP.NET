using AutoMapper;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterProfile>()
                .ReverseMap();
            //CreateMap<Character, ReadCharacterDTO>()
            //    .ForMember(characterReadDTO => characterReadDTO.Movie, opt => opt
            //    .MapFrom(c => c.Movies))
            //    .ReverseMap();

            //CreateMap<Character, CreateCharacterDTO>()
            //    .ForMember(characterReadDTO => characterReadDTO.Movie, opt => opt
            //    .MapFrom(c => c.Movies))
            //    .ReverseMap();

            //CreateMap<Character, UpdateCharacterDTO>()
            //    .ForMember(characterReadDTO => characterReadDTO.Movie, opt => opt
            //    .MapFrom(c => c.Movies))
            //    .ReverseMap();


        }
    }
}
