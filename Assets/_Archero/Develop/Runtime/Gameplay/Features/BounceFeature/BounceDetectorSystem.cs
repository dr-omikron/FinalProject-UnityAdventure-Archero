using System.Collections.Generic;
using System.Linq;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Utilities;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.BounceFeature
{
    public class BounceDetectorSystem : IInitializableSystem, IUpdateableSystem
    {
        private LayerMask _layerToBounceReaction;
        private Buffer<Collider> _contacts;
        private Transform _transform;
        private ReactiveEvent<RaycastHit> _bounceEvent;

        private Vector3 _previousPosition;
        private Collider _previousObject;

        public void OnInit(Entity entity)
        {
            _transform = entity.Transform;
            _layerToBounceReaction = entity.LayerToBounceReaction;
            _contacts = entity.ContactsCollidersBuffer;
            _bounceEvent = entity.BounceEvent;
            _previousPosition = _transform.position;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_contacts.Count > 0)
            {
                List<Collider> bounceContacts = new List<Collider>();

                for (int i = 0; i < _contacts.Count; i++)
                {
                    if(MatchWithBounceLayer(_contacts.Items[i]))
                        bounceContacts.Add(_contacts.Items[i]);
                }

                if (bounceContacts.Any())
                {
                    if(Physics.Raycast(_previousPosition, _transform.forward, out RaycastHit hit, 1000, _layerToBounceReaction))
                    {
                        if (hit.collider != _previousObject && bounceContacts.Contains(hit.collider))
                        {
                            _previousObject = hit.collider;
                            _bounceEvent.Invoke(hit);
                            _previousPosition = _transform.position;
                        }
                    }
                }
                else
                {
                    _previousPosition = _transform.position;
                }
            }
        }

        private bool MatchWithBounceLayer(Collider collider)
        {
            return ((1 << collider.gameObject.layer) & _layerToBounceReaction) != 0;
        }
    }
}
