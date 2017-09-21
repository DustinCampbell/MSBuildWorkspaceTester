using System;
using System.Linq;
using Microsoft.Build.MSBuildLocator;
using Microsoft.Extensions.Logging;

namespace MSBuildWorkspaceTester.Services
{
    internal class MSBuildService
    {
        private readonly ILogger _logger;

        public MSBuildService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MSBuildService>();
        }

        public void Initialize()
        {
            var instances = GetVisualStudioInstances();
            if (instances.Length == 0)
            {
                return;
            }

            var instance = instances[0];
            RegisterVisualStudioInstance(instance);
        }

        private VisualStudioInstance[] GetVisualStudioInstances()
        {
            var instances = MSBuildLocator.QueryVisualStudioInstances().ToArray();
            if (instances.Length == 0)
            {
                _logger.LogError("No MSBuild instances found.");
                return Array.Empty<VisualStudioInstance>();
            }

            _logger.LogInformation("The following MSBuild instances have benen discovered:");
            _logger.LogInformation(string.Empty);

            for (int i = 0; i < instances.Length; i++)
            {
                var instance = instances[i];
                _logger.LogInformation($"    {i + 1}. {instance.Name} ({instance.Version})");
            }

            _logger.LogInformation(string.Empty);

            return instances;
        }

        private void RegisterVisualStudioInstance(VisualStudioInstance instance)
        {
            MSBuildLocator.RegisterInstance(instance);

            _logger.LogInformation("Registered first MSBuild instance:");
            _logger.LogInformation(string.Empty);
            _logger.LogInformation($"    Name: {instance.Name}");
            _logger.LogInformation($"    Version: {instance.Version}");
            _logger.LogInformation($"    VisualStudioRootPath: {instance.VisualStudioRootPath}");
            _logger.LogInformation($"    MSBuildPath: {instance.MSBuildPath}");
        }
    }
}
