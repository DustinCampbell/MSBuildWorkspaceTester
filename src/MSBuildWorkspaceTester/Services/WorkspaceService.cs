using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Logging;

namespace MSBuildWorkspaceTester.Services
{
    internal class WorkspaceService : BaseService
    {
        private readonly MSBuildWorkspace _workspace;

        public WorkspaceService(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _workspace = MSBuildWorkspace.Create();
        }

        public async Task OpenSolutionAsync(string solutionFilePath)
        {
            var watch = Stopwatch.StartNew();
            var solution = await _workspace.OpenSolutionAsync(solutionFilePath);

            watch.Stop();
            Logger.LogInformation($"Solution opened: {watch.Elapsed}");
        }
    }
}
