using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MSBuildWorkspaceTester.Logging;
using MSBuildWorkspaceTester.Services;
using MSBuildWorkspaceTester.ViewModels;

namespace MSBuildWorkspaceTester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();

            var outputService = new OutputService();
            var loggerFactory = new LoggerFactory()
                .AddOutput(outputService);

            serviceCollection.AddSingleton(loggerFactory);
            serviceCollection.AddSingleton<MSBuildService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var msbuildService = serviceProvider.GetRequiredService<MSBuildService>();
            msbuildService.Initialize();

            var mainWindowViewModel = new MainWindowViewModel(serviceProvider);

            this.MainWindow = mainWindowViewModel.CreateView();
            this.MainWindow.Show();
        }
    }
}
