using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.Attack.Shoot
{
    public class DirectionsInstantShootSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _attackDelayEndEvent;
        private Entity _entity;
        private readonly EntitiesFactory _entitiesFactory;

        private ReactiveVariable<float> _damage;
        private Transform _shootPoint;
        private InstantShootingDirectionArgs _directions;

        private IDisposable _attackDelayEndDisposable;

        public DirectionsInstantShootSystem(EntitiesFactory entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;

            _attackDelayEndEvent = entity.AttackDelayEndEvent;
            _directions = entity.InstantShootingDirection;

            _damage = entity.InstantAttackDamage;
            _shootPoint = entity.ShootPoint;

            _attackDelayEndDisposable = _attackDelayEndEvent.Subscribe(OnAttackDelayEnd);
        }

        private void OnAttackDelayEnd()
        {
            foreach (InstantShotDirectionArgs arg in _directions.Args)
                Shoot(arg.Angle, arg.ProjectileCount);
        }

        private void Shoot(int argAngle, int argProjectileCount)
        {
            Vector3 directionForShoot = Quaternion.Euler(new Vector3(0, argAngle, 0)) * _shootPoint.forward;
            Vector2 perpendicular = Vector2.Perpendicular(new Vector2(directionForShoot.x, directionForShoot.z)).normalized;

            float offsetBetweenProjectiles = 0.6f;

            for (int i = 0; i < argProjectileCount; i++)
            {
                Vector2 offset = perpendicular * (-offsetBetweenProjectiles / 2 * (argProjectileCount - 1) + i * offsetBetweenProjectiles);
                Vector3 position = new Vector3(_shootPoint.position.x + offset.x, _shootPoint.position.y, _shootPoint.position.z + offset.y);

                _entitiesFactory.CreateProjectile(position, directionForShoot, _damage.Value, _entity);
            }
        }

        public void OnDispose()
        {
            _attackDelayEndDisposable.Dispose();
        }
    }
}
