using System;
using System.Linq;
using Microsoft.Build.MSBuildLocator;

namespace MSBuildWorkspaceTester
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintBanner();

            var instances = GetVisualStudioInstances();
            if (instances.Length == 0)
            {
                return;
            }

            var instance = instances[0];
            RegisterVisualStudioInstance(instance);
        }

        private static void PrintBanner()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("--- MSBuildWorkspace Tester ---");
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
        }

        private static VisualStudioInstance[] GetVisualStudioInstances()
        {
            var instances = MSBuildLocator.QueryVisualStudioInstances().ToArray();
            if (instances.Length == 0)
            {
                Console.WriteLine("No MSBuild instances found.");
                return Array.Empty<VisualStudioInstance>();
            }

            Console.WriteLine("The following MSBuild instances have been discovered:");
            Console.WriteLine();

            for (int i = 0; i < instances.Length; i++)
            {
                var instance = instances[i];
                Console.WriteLine($"    {i + 1}. {instance.Name} ({instance.Version})");
            }

            Console.WriteLine();

            return instances;
        }

        private static void RegisterVisualStudioInstance(VisualStudioInstance selectedInstance)
        {
            MSBuildLocator.RegisterInstance(selectedInstance);

            Console.WriteLine($"Registered first instance:");
            Console.WriteLine();
            Console.WriteLine($"    Name: {selectedInstance.Name}");
            Console.WriteLine($"    Version: {selectedInstance.Version}");
            Console.WriteLine($"    VisualStudioRootPath: {selectedInstance.VisualStudioRootPath}");
            Console.WriteLine($"    MSBuildPath: {selectedInstance.MSBuildPath}");
            Console.WriteLine();
        }
    }
}
