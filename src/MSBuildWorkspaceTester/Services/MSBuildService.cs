﻿using System;
using System.Linq;
using Microsoft.Build.Locator;
using Microsoft.Extensions.Logging;

namespace MSBuildWorkspaceTester.Services
{
    internal class MSBuildService : BaseService
    {
        public MSBuildService(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
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
                Logger.LogError("No MSBuild instances found.");
                return Array.Empty<VisualStudioInstance>();
            }

            Logger.LogInformation("The following MSBuild instances have benen discovered:");
            Logger.LogInformation(string.Empty);

            for (int i = 0; i < instances.Length; i++)
            {
                var instance = instances[i];
                Logger.LogInformation($"    {i + 1}. {instance.Name} ({instance.Version})");
            }

            Logger.LogInformation(string.Empty);

            return instances;
        }

        private void RegisterVisualStudioInstance(VisualStudioInstance instance)
        {
            MSBuildLocator.RegisterInstance(instance);

            Logger.LogInformation("Registered first MSBuild instance:");
            Logger.LogInformation(string.Empty);
            Logger.LogInformation($"    Name: {instance.Name}");
            Logger.LogInformation($"    Version: {instance.Version}");
            Logger.LogInformation($"    VisualStudioRootPath: {instance.VisualStudioRootPath}");
            Logger.LogInformation($"    MSBuildPath: {instance.MSBuildPath}");
            Logger.LogInformation(string.Empty);
        }
    }
}
