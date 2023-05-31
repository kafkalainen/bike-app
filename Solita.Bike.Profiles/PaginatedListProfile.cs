using AutoMapper;

namespace Solita.Bike.Shared.Profiles;

public class PaginatedListProfile : Profile
{
    public PaginatedListProfile()
    {
        CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>))
            .IncludeAllDerived()
            .ConvertUsing(typeof(PaginatedListConverter<,>));
    }
}