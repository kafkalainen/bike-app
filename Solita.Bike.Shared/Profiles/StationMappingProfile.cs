using AutoMapper;
using Solita.Bike.Shared.Dtos;
using Solita.Bike.Shared.Models;

namespace Solita.Bike.Shared.Profiles;

public class StationMappingProfile : Profile
{
    public StationMappingProfile()
    {
        CreateMap<Station, SingleStationInfo>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src =>
                    new List<Localization>
                    {
                        new() { Identifier = "fi", Value = src.NameInFinnish },
                        new() { Identifier = "en", Value = src.NameInEnglish },
                        new() { Identifier = "se", Value = src.NameInSwedish }
                    }))
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src =>
                    new List<Localization>
                    {
                        new() { Identifier = "fi", Value = src.AddressInFinnish },
                        new() { Identifier = "en", Value = src.AddressInSwedish }
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
                    new List<Localization>
                    {
                        new() { Identifier = "fi", Value = src.NameInFinnish },
                        new() { Identifier = "en", Value = src.NameInEnglish },
                        new() { Identifier = "se", Value = src.NameInSwedish }
                    }))
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src =>
                    new List<Localization>
                    {
                        new() { Identifier = "fi", Value = src.AddressInFinnish },
                        new() { Identifier = "se", Value = src.AddressInSwedish }
                    }))
            .ForMember(dest => dest.City,
                opt => opt.MapFrom(src =>
                    new List<Localization>
                    {
                        new() { Identifier = "fi", Value = src.CityInFinnish },
                        new() { Identifier = "se", Value = src.CityInSwedish }
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