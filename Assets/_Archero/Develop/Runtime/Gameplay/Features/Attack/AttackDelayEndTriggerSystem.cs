using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.Attack
{
    public class AttackDelayEndTriggerSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _attackDelayEndEvent;
        private ReactiveEvent _startAttackEvent;
        private ReactiveVariable<float> _delay;
        private ReactiveVariable<float> _attackProcessCurrentTime;

        private bool _alreadyAttacked;

        private IDisposable _timerDisposable;
        private IDisposable _startAttackDisposable;

        public void OnInit(Entity entity)
        {
            _attackDelayEndEvent = entity.AttackDelayEndEvent;
            _delay = entity.AttackDelayTime;
            _attackProcessCurrentTime = entity.AttackProcessCurrentTime;
            _startAttackEvent = entity.StartAttackEvent;

            _timerDisposable = _attackProcessCurrentTime.Subscribe(OnTimerChanged);
            _startAttackDisposable = _startAttackEvent.Subscribe(OnStartAttack);
        }

        public void OnDispose()
        {
            _timerDisposable.Dispose();
            _startAttackDisposable.Dispose();
        }

        private void OnTimerChanged(float arg1, float currentTime)
        {
            if (_alreadyAttacked)
                return;

            if (currentTime >= _delay.Value)
            {
                _attackDelayEndEvent.Invoke();
                _alreadyAttacked = true;
                Debug.Log("Attack Delay End");
            }
        }

        private void OnStartAttack()
        {
            _alreadyAttacked = false;
        }
    }
}
