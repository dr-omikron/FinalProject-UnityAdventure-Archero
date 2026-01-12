using System;
using System.Collections;
using System.Collections.Generic;

namespace _Archero.Develop.Runtime.Utilities.ConfigsManagement
{
    public interface IConfigsLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded);
    }
}
