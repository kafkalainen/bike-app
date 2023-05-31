using AutoMapper;
using Solita.Bike.Models;
using Solita.Bike.Shared;
using Solita.Bike.Shared.Dtos;

namespace Solita.Bike.Profiles;

public class StationMappingProfile : Profile
{
    public StationMappingProfile()
    {
        CreateMap<Station, SingleStationInfo>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src =>
                    new Dictionary<Localization, string?>
                    {
                        { Localization.Fi, src.NameInFinnish },
                        { Localization.Sv, src.NameInSwedish },
                        { Localization.En, src.NameInEnglish }
                    }))
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src =>
                    new Dictionary<Localization, string?>
                    {
                        { Localization.Fi, src.AddressInFinnish },
                        { Localization.Sv, src.AddressInSwedish }
                    }))
            .ForMember(dest => dest.StartJourneyTotal,
                opt => opt.MapFrom(src =>
                    src.DepartureJourneys.Count()))
            .ForMember(dest => dest.EndJourneyTotal,
            opt => opt.MapFrom(src =>
                src.ReturnJourneys.Count()));

        CreateMap<Station, StationInfo>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src =>
                    new Dictionary<Localization, string?>
                    {
                        { Localization.Fi, src.NameInFinnish },
                        { Localization.Sv, src.NameInSwedish },
                        { Localization.En, src.NameInEnglish }
                    }))
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src =>
                    new Dictionary<Localization, string?>
                    {
                        { Localization.Fi, src.AddressInFinnish },
                        { Localization.Sv, src.AddressInSwedish }
                    }))
            .ForMember(dest => dest.City,
                opt => opt.MapFrom(src =>
                    new Dictionary<Localization, string?>
                    {
                        { Localization.Fi, src.CityInFinnish },
                        { Localization.Sv, src.CityInSwedish }
                    }))
            .ForMember(dest => dest.Operator,
                opt => opt.MapFrom(src =>
                    src.Operator))
            .ForMember(dest => dest.Capacity,
                opt => opt.MapFrom(src =>
                    src.Capacity))
            .ForMember(dest => dest.X,
                opt => opt.MapFrom(src =>
                    src.X))
            .ForMember(dest => dest.Y,
                opt => opt.MapFrom(src =>
                    src.Y));
        CreateMap<PaginatedList<Station>, PaginatedList<StationInfo>>().ConvertUsing<PaginatedListConverter<Station, StationInfo>>();
    }
}