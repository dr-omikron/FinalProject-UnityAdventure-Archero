using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.Sensors
{
    public class DeathMaskTouchDetectorSystem : IInitializableSystem, IUpdateableSystem
    {
        private Buffer<Collider> _contacts;
        private ReactiveVariable<bool> _isTouchDeathMask;
        private LayerMask _deathMask;

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactsCollidersBuffer;
            _isTouchDeathMask = entity.IsTouchDeathMask;
            _deathMask = entity.DeathMask;
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _contacts.Count; i++)
            {
                if (MatchWithDeathLayer(_contacts.Items[i]))
                {
                    _isTouchDeathMask.Value = true;
                    return;
                }
            }

            _isTouchDeathMask.Value = false;
        }

        private bool MatchWithDeathLayer(Collider collider)
        {
            return ((1 << collider.gameObject.layer) & _deathMask) != 0;
        }
    }
}
