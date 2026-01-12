using System;
using System.Collections.Generic;

namespace _Archero.Develop.Runtime.Utilities.ConfigsManagement
{
    public class ConfigsProviderService
    {
        private readonly Dictionary<Type, object> _configs = new Dictionary<Type, object>();

        public T GetConfig<T>() where T : class
        {
            if (_configs.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException($"Not found config by {typeof(T)}");

            return (T)_configs[typeof(T)];
        }
    }
}
