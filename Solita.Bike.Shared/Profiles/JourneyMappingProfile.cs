
using Solita.Bike.Shared.Dtos;
using Solita.Bike.Shared.Models;

namespace Solita.Bike.Shared.Profiles
{
    using AutoMapper;
    
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