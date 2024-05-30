using Assignment.Application.Common.Constants;
using Assignment.Application.Common.Interfaces;
using Assignment.Application.Common.Security;

namespace Assignment.Application.TodoLists.Queries.GetTodos;

[Authorize]
public record GetTodosQuery : IRequest<IList<TodoListDto>>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, IList<TodoListDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICache _cache;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(IApplicationDbContext context, ICache cache, IMapper mapper)
    {
        _context = context;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<IList<TodoListDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var timestamps = await _context.TodoLists.Select(tl => tl.LastModified).ToListAsync();
        var result = _cache.GetValue<List<TodoListDto>>(CacheKeys.TodoLists, timestamps.Select(t => t.UtcDateTime).Max());
        if (result == null)
        {
            result = await _context.TodoLists
                .AsNoTracking()
                .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken);

            _cache.AddValue(CacheKeys.TodoLists, result);
        }

        return result;
    }
}
