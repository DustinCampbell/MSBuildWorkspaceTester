using System;
using System.Windows;
using System.Windows.Controls;

namespace MSBuildWorkspaceTester.ViewModels
{
    internal abstract class ViewModel<TView> : ViewModel
        where TView : ContentControl
    {
        private readonly string _viewName;
        private TView _view;

        protected IServiceProvider ServiceProvider { get; }

        protected ViewModel(string viewName, IServiceProvider serviceProvider)
        {
            _viewName = viewName ?? throw new ArgumentNullException(nameof(viewName));
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected TView View => _view;

        protected virtual void OnViewCreated(TView view)
        {
            // Decendents can override
        }

        private string GetViewUriString()
            => $"/{GetType().Assembly.GetName().Name};component/Views/{_viewName}.xaml";

        public TView CreateView()
        {
            var uri = new Uri(GetViewUriString(), UriKind.Relative);
            _view = (TView)Application.LoadComponent(uri);
            _view.DataContext = this;

            OnViewCreated(_view);

            return _view;
        }
    }
}
