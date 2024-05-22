using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Jmeza44.EtherBlog.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var projectName = Assembly.GetEntryAssembly()?.GetName().Name;

            _logger.LogInformation("{ProjectName} Request: {Name}_{@Request}",
                projectName, requestName, request);

            return Task.CompletedTask;
        }
    }
}
