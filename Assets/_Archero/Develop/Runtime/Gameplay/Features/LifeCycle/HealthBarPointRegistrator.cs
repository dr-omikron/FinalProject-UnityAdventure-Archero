using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class HealthBarPointRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _point;

        public override void Register(Entity entity)
        {
            entity.AddHealthBarPoint(_point);
        }
    }
}
