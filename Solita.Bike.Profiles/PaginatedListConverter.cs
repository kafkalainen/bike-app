using AutoMapper;
using Solita.Bike.Shared;

namespace Solita.Bike.Profiles;

public class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>>
{
    private readonly IMapper m_mapper;

    public PaginatedListConverter(IMapper mapper)
    {
        m_mapper = mapper;
    }

    public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
    {
        var mappedItems = m_mapper.Map<List<TSource>, List<TDestination>>(source);
        return new PaginatedList<TDestination>(mappedItems, source.TotalPages, source.PageIndex, source.PageSize);
    }
}
