using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Conditions;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.Attack
{
    public class AttackCanceledSystem : IInitializableSystem, IUpdateableSystem
    {
        private ReactiveVariable<bool> _inAttackProcess;
        private ReactiveEvent _attackCanceledEvent;
        private ICompositeCondition _mustCanceledAttack;

        public void OnInit(Entity entity)
        {
            _inAttackProcess = entity.InAttackProcess;
            _attackCanceledEvent = entity.AttackCanceledEvent;
            _mustCanceledAttack = entity.MustCanceledAttack;
        }

        public void OnUpdate(float deltaTime)
        {
            if(_inAttackProcess.Value == false)
                return;

            if (_mustCanceledAttack.Evaluate())
            {
                _inAttackProcess.Value = false;
                _attackCanceledEvent.Invoke();
                Debug.Log("Attack Canceled");
            }
        }
    }
}
