using Assignment.Application.Common.Interfaces;

namespace Assignment.Application.WatherForecast.Commands.GetCities;
public class GetCountriesCommand : IRequest<List<CountryDto>>
{
}

public class GetCountriesCommandHandler : IRequestHandler<GetCountriesCommand, List<CountryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCountriesCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CountryDto>> Handle(GetCountriesCommand request, CancellationToken cancellationToken)
    {
        return await _context
                    .Countries
                    .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken);
    }
}
