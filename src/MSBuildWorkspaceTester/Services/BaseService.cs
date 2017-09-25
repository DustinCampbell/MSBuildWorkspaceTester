using Microsoft.Extensions.Logging;

namespace MSBuildWorkspaceTester.Services
{
    internal abstract class BaseService
    {
        protected readonly ILogger Logger;

        protected BaseService(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType());
        }
    }
}
