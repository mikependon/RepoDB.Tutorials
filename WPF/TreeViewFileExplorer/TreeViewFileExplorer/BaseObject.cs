using System;
using System.Collections.Generic;

namespace TreeViewFileExplorer
{
    [Serializable]
    public abstract class BaseObject : PropertyNotifier
    {
        private IDictionary<string, object> m_values = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);

        public BaseObject()
        {
        }

        public T GetValue<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }
            var value = this.GetValue(key);
            if (value is T)
            {
                return (T)value;
            }
            return default(T);
        }

        private object GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            if (this.m_values.ContainsKey(key))
            {
                return this.m_values[key];
            }
            return null;
        }

        public void SetValue(string key, object value)
        {
            if (!this.m_values.ContainsKey(key))
            {
                this.m_values.Add(key, value);
            }
            else
            {
                this.m_values[key] = value;
            }
            base.OnPropertyChanged(key);
        }
    }
}
