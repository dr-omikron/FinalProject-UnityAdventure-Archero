using System;
using System.Collections.Generic;

namespace _Archero.Develop.Runtime.Gameplay.EntitiesCore
{
    public class Entity
    {
        private readonly Dictionary<Type, IEntityComponent> _components = new Dictionary<Type, IEntityComponent>();

        public void AddComponent<TComponent>(TComponent component) where TComponent : class, IEntityComponent
        {
            _components.Add(typeof(TComponent), component);
        }

        public bool HasComponent<TComponent>() where TComponent : class, IEntityComponent
            => _components.ContainsKey(typeof(TComponent));

        public bool TryGetComponent<TComponent>(out TComponent component) where TComponent : class, IEntityComponent
        {
            if (_components.TryGetValue(typeof(TComponent), out IEntityComponent foundObject))
            {
                component = (TComponent)foundObject;
                return true;
            }

            component = null;
            return false;
        }

        public TComponent GetComponent<TComponent>() where TComponent : class, IEntityComponent
        {
            if (TryGetComponent(out TComponent component) == false)
                throw new ArgumentException($"Entity not exist {typeof(TComponent)}");

            return component;
        }
    }
}
