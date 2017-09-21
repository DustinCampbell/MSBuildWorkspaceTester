using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MSBuildWorkspaceTester.Services;

namespace MSBuildWorkspaceTester.ViewModels
{
    internal class MainWindowViewModel : ViewModel<Window>
    {
        private readonly ILogger _logger;
        private readonly OutputService _outputService;

        public ICommand OpenProjectCommand { get; }

        public MainWindowViewModel(IServiceProvider serviceProvider)
            : base("MainWindowView", serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<MainWindowViewModel>();

            _outputService = serviceProvider.GetRequiredService<OutputService>();
            _outputService.TextChanged += delegate { PropertyChanged("OutputText"); };

            OpenProjectCommand = RegisterCommand(
                text: "Open Project",
                name: "Open Project",
                executed: OpenProjectExecuted,
                canExecute: CanOpenProjectExecute);
        }

        private bool CanOpenProjectExecute() => true;

        private void OpenProjectExecuted()
        {
            _logger.LogInformation("Open Project");
        }

        public string OutputText => _outputService.GetText();
    }
}
