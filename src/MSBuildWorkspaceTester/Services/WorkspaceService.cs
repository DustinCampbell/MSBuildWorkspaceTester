using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Logging;

namespace MSBuildWorkspaceTester.Services
{
    internal class WorkspaceService : BaseService
    {
        public MSBuildWorkspace Workspace { get; }

        public WorkspaceService(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            Workspace = MSBuildWorkspace.Create();
            Workspace.WorkspaceFailed += WorkspaceFailed;
        }

        private void WorkspaceFailed(object sender, WorkspaceDiagnosticEventArgs e)
        {
            if (e.Diagnostic.Kind == WorkspaceDiagnosticKind.Failure)
            {
                Logger.LogError(e.Diagnostic.Message);
            }
            else
            {
                Logger.LogWarning(e.Diagnostic.Message);
            }
        }

        public async Task OpenSolutionAsync(string solutionFilePath)
        {
            var watch = Stopwatch.StartNew();
            var solution = await Workspace.OpenSolutionAsync(solutionFilePath);

            watch.Stop();
            Logger.LogInformation($"Solution opened: {watch.Elapsed}");
        }

        public async Task OpenProjectAsync(string projectFilePath)
        {
            var watch = Stopwatch.StartNew();
            var project = await Workspace.OpenProjectAsync(projectFilePath);

            watch.Stop();
            Logger.LogInformation($"Project opened: {watch.Elapsed}");
        }
    }
}
