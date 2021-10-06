using AutoMapper;
using PlatformWellAssessment.Dtos;
using PlatformWellAssessment.Models;

namespace PlatformWellAssessment.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            //Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformUpdateDto, Platform>();
            CreateMap<Platform, PlatformUpdateDto>();
        }
    }
}
