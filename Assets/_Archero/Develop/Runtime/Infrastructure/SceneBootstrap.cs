using System.Collections;
using _Archero.Develop.Runtime.Infrastructure.DI;
using _Archero.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace _Archero.Develop.Runtime.Infrastructure
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract void ProcessRegistration(DIContainer container, IInputSceneArgs sceneArgs = null);
        public abstract IEnumerator Initialize();
        public abstract void Run();
    }
}
