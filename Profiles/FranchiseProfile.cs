using AutoMapper;
using MovieWebASP.NET.DTO.Franchise;
using MovieWebASP.NET.Models;

namespace MovieWebASP.NET.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, ReadFranchiseDTO>()
                    .ReverseMap();

            CreateMap<Franchise, CreateFranchiseDTO>()
                    .ReverseMap();
            CreateMap<Franchise, UpdateFranchiseDTO>()
                    .ReverseMap();

        }
    }
}
