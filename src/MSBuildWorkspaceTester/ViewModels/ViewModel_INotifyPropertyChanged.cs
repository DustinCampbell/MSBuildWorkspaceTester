using System.Collections.Generic;
using System.ComponentModel;

namespace MSBuildWorkspaceTester.ViewModels
{
    internal abstract partial class ViewModel : INotifyPropertyChanged
    {
        private static readonly Dictionary<string, PropertyChangedEventArgs> s_eventArgsCache
            = new Dictionary<string, PropertyChangedEventArgs>();

        private static PropertyChangedEventArgs GetEventArgs(string propertyName)
        {
            lock (s_eventArgsCache)
            {
                if (!s_eventArgsCache.TryGetValue(propertyName, out var eventArgs))
                {
                    eventArgs = new PropertyChangedEventArgs(propertyName);
                    s_eventArgsCache.Add(propertyName, eventArgs);
                }

                return eventArgs;
            }
        }

        private PropertyChangedEventHandler _propertyChangedHandler;

        protected void PropertyChanged(string propertyName)
        {
            _propertyChangedHandler?.Invoke(this, GetEventArgs(propertyName));
        }

        protected void AllPropertiesChanged()
        {
            PropertyChanged(string.Empty);
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { _propertyChangedHandler += _propertyChangedHandler; }
            remove { _propertyChangedHandler += _propertyChangedHandler; }
        }
    }
}
