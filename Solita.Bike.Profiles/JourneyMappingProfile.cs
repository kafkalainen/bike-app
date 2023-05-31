using AutoMapper;
using Solita.Bike.Models;
using Solita.Bike.Shared;
using Solita.Bike.Shared.Dtos;

namespace Solita.Bike.Profiles
{
    public class JourneyMappingProfile : Profile
    {
        public JourneyMappingProfile()
        {
            CreateMap<Journey, JourneyInfo>()
            .ForMember(dest => dest.DepartureStation, opt => opt.MapFrom(src => src.DepartureStationName))
            .ForMember(dest => dest.ReturnStation, opt => opt.MapFrom(src => src.ReturnStationName))
            .ForMember(dest => dest.CoveredDistanceInKilometers, opt => opt.MapFrom(src => src.CoveredDistanceInMeters / 1000))
            .ForMember(dest => dest.DurationInMinutes, opt => opt.MapFrom(src => src.DurationInSeconds / 60));
            CreateMap<PaginatedList<Journey>, PaginatedList<JourneyInfo>>().ConvertUsing<PaginatedListConverter<Journey, JourneyInfo>>();
        }
    }
}