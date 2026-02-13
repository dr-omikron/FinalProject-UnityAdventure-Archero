using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Conditions;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class RigidbodyRotationSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<Vector3> _direction;
        private ReactiveVariable<float> _rotationSpeed;
        private Rigidbody _rigidbody;

        private ICompositeCondition _canRotate;

        public void OnInit(Entity entity)
        {
            _rigidbody = entity.Rigidbody;
            _rotationSpeed = entity.RotationSpeed;
            _direction = entity.RotationDirection;
            _canRotate = entity.CanRotate;

            if(_direction.Value != Vector3.zero)
                _rigidbody.transform.rotation = Quaternion.LookRotation(_direction.Value.normalized);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canRotate.Evaluate() == false)
                return;

            if(_direction.Value == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_direction.Value.normalized);
            float step = _rotationSpeed.Value * deltaTime;

            Quaternion rotation = Quaternion.RotateTowards(_rigidbody.rotation, lookRotation, step);
            _rigidbody.MoveRotation(rotation);
        }
    }
}
