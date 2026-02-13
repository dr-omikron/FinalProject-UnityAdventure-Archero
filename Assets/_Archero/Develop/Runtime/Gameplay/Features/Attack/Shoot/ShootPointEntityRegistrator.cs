using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.Attack.Shoot
{
    public class ShootPointEntityRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _shootPoint;

        public override void Register(Entity entity)
        {
            entity.AddShootPoint(_shootPoint);
        }
    }
}
