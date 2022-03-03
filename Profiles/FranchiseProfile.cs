using AutoMapper;
using TestWebASP.NET.DTO.Franchise;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, ReadFranchiseDTO>()
                    .ReverseMap();

            CreateMap<Franchise, CreateFranchiseDTO>()
                    .ReverseMap();

        }
    }
}
