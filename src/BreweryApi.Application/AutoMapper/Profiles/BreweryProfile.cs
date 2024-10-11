using AutoMapper;
using BreweryApi.Application.AutoMapper.DTOs;
using BreweryApi.Domain.Entities;

namespace BreweryApi.Application.AutoMapper.Profiles;

public class BreweryProfile : Profile
{
    public BreweryProfile()
    {
        CreateMap<Brewery, BreweryDto>()
            .ForMember(
                dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(
                dest => dest.Location,
                src => src.MapFrom(x => x.City + ", " + x.State))
            .ForMember(
                dest => dest.WebsiteUrl,
                src => src.MapFrom(x => x.WebsiteUrl));
    }
}