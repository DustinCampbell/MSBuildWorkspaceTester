using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using MSBuildWorkspaceTester.Services;

namespace MSBuildWorkspaceTester.ViewModels
{
    internal class MainWindowViewModel : ViewModel<Window>
    {
        private readonly ILogger _logger;
        private readonly OutputService _outputService;
        private readonly WorkspaceService _workspaceService;

        public ICommand OpenProjectCommand { get; }

        public MainWindowViewModel(IServiceProvider serviceProvider)
            : base("MainWindowView", serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<MainWindowViewModel>();

            _outputService = serviceProvider.GetRequiredService<OutputService>();
            _outputService.TextChanged += delegate { PropertyChanged("OutputText"); };

            _workspaceService = serviceProvider.GetRequiredService<WorkspaceService>();

            OpenProjectCommand = RegisterCommand(
                text: "Open Project/Solution",
                name: "Open Project/Solution",
                executed: OpenProjectExecuted,
                canExecute: CanOpenProjectExecute);
        }

        private bool CanOpenProjectExecute() => true;

        private void OpenProjectExecuted()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Open Project or Solution",
                Filter = "Supported Files (*.sln,*.csproj,*.vbproj)|*.sln;*.csproj;*.vbproj|" +
                         "Solution Files (*.sln)||*.sln|" +
                         "Project Files (*.csproj,*.vbproj)|*.csproj|*.vbproj|" +
                         "All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog(this.View) == true)
            {
                _workspaceService.OpenSolutionAsync(dialog.FileName).ConfigureAwait(continueOnCapturedContext: true);
            }
        }

        public string OutputText => _outputService.GetText();
    }
}
