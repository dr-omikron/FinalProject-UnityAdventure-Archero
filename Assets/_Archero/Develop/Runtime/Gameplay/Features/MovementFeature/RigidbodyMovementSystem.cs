using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Conditions;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class RigidbodyMovementSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private ReactiveVariable<bool> _isMoving;
        private Rigidbody _rigidbody;

        private ICompositeCondition _canMove;

        public void OnInit(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _moveSpeed = entity.MoveSpeed;
            _isMoving = entity.IsMoving;
            _rigidbody = entity.Rigidbody;
            _canMove = entity.CanMove;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canMove.Evaluate() == false)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }

            Vector3 velocity = _moveDirection.Value.normalized * _moveSpeed.Value;
            _isMoving.Value = velocity.magnitude > 0;
            _rigidbody.velocity = velocity;
        }
    }
}
