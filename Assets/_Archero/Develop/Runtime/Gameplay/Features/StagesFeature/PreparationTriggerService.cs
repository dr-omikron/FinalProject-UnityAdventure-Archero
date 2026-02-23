using System;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.MainHero;
using _Archero.Develop.Runtime.Utilities;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class PreparationTriggerService
    {
        private readonly ReactiveVariable<bool> _hasMainHeroContact = new ReactiveVariable<bool>();

        private readonly EntitiesFactory _entitiesFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        private Entity _nextStageTrigger;
        private Buffer<Entity> _nextStageTriggerContacts;

        public PreparationTriggerService(EntitiesFactory entitiesFactory, EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesFactory = entitiesFactory;
            _entitiesLifeContext = entitiesLifeContext;
        }

        public IReadOnlyVariable<bool> HasMainHeroContact => _hasMainHeroContact;

        public void Create(Vector3 position)
        {
            if (_nextStageTrigger != null)
                throw new InvalidOperationException("Trigger already created");

            _nextStageTrigger = _entitiesFactory.CreateContactTrigger(position);
            _nextStageTriggerContacts = _nextStageTrigger.ContactsEntitiesBuffer;
        }

        public void Update(float deltaTime)
        {
            if(_nextStageTrigger == null)
                return;

            for (int i = 0; i < _nextStageTriggerContacts.Count; i++)
            {
                Entity contact = _nextStageTriggerContacts.Items[i];

                if (contact.HasComponent<IsMainHero>())
                {
                    _hasMainHeroContact.Value = true;
                    return;
                }
            }

            _hasMainHeroContact.Value = false;
        }

        public void Cleanup()
        {
            _entitiesLifeContext.Release(_nextStageTrigger);
            _hasMainHeroContact.Value = false;
            _nextStageTrigger = null;
            _nextStageTriggerContacts = null;
        }
    }
}
