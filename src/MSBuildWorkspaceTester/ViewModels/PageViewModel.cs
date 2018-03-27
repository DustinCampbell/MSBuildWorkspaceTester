using System;
using System.Windows;
using System.Windows.Controls;

namespace MSBuildWorkspaceTester.ViewModels
{
    internal abstract class PageViewModel : ViewModel
    {
        private string _caption;

        public string Caption
        {
            get => _caption;
            protected set
            {
                SetValue(ref _caption, value);
            }
        }
    }

    internal abstract class PageViewModel<TContent> : PageViewModel
        where TContent : ContentControl
    {
        private readonly string _contentName;

        protected IServiceProvider ServiceProvider { get; }
        public TContent Content { get; private set; }

        protected PageViewModel(string contentName, IServiceProvider serviceProvider)
        {
            _contentName = contentName ?? throw new ArgumentNullException(nameof(contentName));
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            CreateContent();
        }

        protected virtual void OnContentCreated(TContent content)
        {
            // Decendents can override
        }

        private string GetViewUriString()
            => $"/{GetType().Assembly.GetName().Name};component/Views/{_contentName}.xaml";

        public TContent CreateContent()
        {
            var uri = new Uri(GetViewUriString(), UriKind.Relative);
            Content = (TContent)Application.LoadComponent(uri);
            Content.DataContext = this;

            OnContentCreated(Content);

            return Content;
        }
    }
}
