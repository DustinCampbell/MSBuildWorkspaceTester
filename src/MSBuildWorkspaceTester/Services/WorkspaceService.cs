using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
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
            _workspace.WorkspaceFailed += WorkspaceFailed;
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
            var solution = await _workspace.OpenSolutionAsync(solutionFilePath);

            watch.Stop();
            Logger.LogInformation($"Solution opened: {watch.Elapsed}");
        }

        public async Task OpenProjectAsync(string projectFilePath)
        {
            var watch = Stopwatch.StartNew();
            var project = await _workspace.OpenProjectAsync(projectFilePath);

            watch.Stop();
            Logger.LogInformation($"Project opened: {watch.Elapsed}");
        }
    }
}
