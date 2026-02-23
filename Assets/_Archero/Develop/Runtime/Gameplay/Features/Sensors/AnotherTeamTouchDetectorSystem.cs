using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Archero.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Archero.Develop.Runtime.Utilities;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.Sensors
{
    public class AnotherTeamTouchDetectorSystem : IInitializableSystem, IUpdateableSystem
    {
        private Buffer<Entity> _contacts;
        private ReactiveVariable<bool> _isTouchAnotherTeam;
        private ReactiveVariable<Teams> _sourceTeam;

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactsEntitiesBuffer;
            _isTouchAnotherTeam = entity.IsTouchAnotherTeam;
            _sourceTeam = entity.Team;
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _contacts.Count; i++)
            {
                Entity contact = _contacts.Items[i];

                if (contact.TryGetTeam(out ReactiveVariable<Teams> anotherTeam))
                {
                    if (_sourceTeam.Value != anotherTeam.Value)
                    {
                        _isTouchAnotherTeam.Value = true;
                        return;
                    }
                }
            }

            _isTouchAnotherTeam.Value = false;
        }
    }
}
