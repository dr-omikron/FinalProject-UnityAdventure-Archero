using System.Collections;
using UnityEngine;

namespace _Archero.Develop.Runtime.Utilities.CoroutinesManagement
{
    public interface ICoroutinesPerformer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);
        void StopPerform(Coroutine coroutine);
    }
}
