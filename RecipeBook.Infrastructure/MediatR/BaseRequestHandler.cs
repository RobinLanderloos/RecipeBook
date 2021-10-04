using MediatR;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;

namespace RecipeBook.Infrastructure.MediatR
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly RecipeBookContext _context;
        protected readonly ILogger _logger;

        protected BaseRequestHandler(RecipeBookContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        protected void LogHandling(string requestName)
        {
            _logger.LogInformation("Handling " + requestName);
        }
    }
}
