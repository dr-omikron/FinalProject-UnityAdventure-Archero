using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
    public abstract class EntityView : MonoBehaviour
    {
        public void Link(Entity entity)
        {
            entity.Initialized += OnEntityStartedWork;
        }

        public virtual void Cleanup(Entity entity)
        {
            entity.Initialized -= OnEntityStartedWork;
        }

        protected abstract void OnEntityStartedWork(Entity entity);
    }
}
