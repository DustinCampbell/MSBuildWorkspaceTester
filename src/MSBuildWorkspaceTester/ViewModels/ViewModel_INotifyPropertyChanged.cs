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
            var eventArgs = GetEventArgs(propertyName);
            _propertyChangedHandler?.Invoke(this, eventArgs);
        }

        protected void AllPropertiesChanged()
        {
            PropertyChanged(string.Empty);
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { _propertyChangedHandler += value; }
            remove { _propertyChangedHandler -= value; }
        }
    }
}
