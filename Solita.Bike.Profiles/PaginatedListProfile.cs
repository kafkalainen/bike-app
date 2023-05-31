using AutoMapper;
using Solita.Bike.Shared;

namespace Solita.Bike.Profiles;

public class PaginatedListProfile : Profile
{
    public PaginatedListProfile()
    {
        CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>))
            .IncludeAllDerived()
            .ConvertUsing(typeof(PaginatedListConverter<,>));
    }
}