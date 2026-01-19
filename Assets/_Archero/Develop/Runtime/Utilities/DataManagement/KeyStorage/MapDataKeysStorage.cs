using System;
using System.Collections.Generic;

namespace _Archero.Develop.Runtime.Utilities.DataManagement.KeyStorage
{
    public class MapDataKeysStorage : IDataKeysStorage
    {
        private readonly Dictionary<Type, string> _keys = new Dictionary<Type, string>
        {
            { typeof(PlayerData), "PlayerData" }
        };

        public string GetKeyFor<TData>() where TData : ISaveData => _keys[typeof(TData)];
    }
}
