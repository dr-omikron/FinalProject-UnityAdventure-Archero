using System;
using _Archero.Develop.Runtime.Configs.Abilities;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.Attack;
using _Archero.Develop.Runtime.Gameplay.Features.BounceFeature;
using _Archero.Develop.Runtime.Utilities.Conditions;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class BounceProjectileAbility : Ability, IDisposable
    {
        private BounceProjectileAbilityConfig _config;
        private Entity _owner;
        private EntitiesLifeContext _entitiesLifeContext;

        public BounceProjectileAbility(
            BounceProjectileAbilityConfig config, 
            Entity owner, 
            EntitiesLifeContext entitiesLifeContext, 
            int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _config = config;
            _owner = owner;
            _entitiesLifeContext = entitiesLifeContext;
        }

        public override void Activate()
        {
            _entitiesLifeContext.Added += OnCreaturesAdded;
        }

        private void OnCreaturesAdded(Entity entity)
        {
            if (entity.HasComponent<IsProjectile>()
                && entity.TryGetOwner(out ReactiveVariable<Entity> owner)
                && owner.Value == _owner)
            {
                entity
                    .AddBounceCount(new ReactiveVariable<int>(_config.GetBounceCountBy(CurrentLevel.Value)))
                    .AddBounceEvent()
                    .AddLayerToBounceReaction(_config.LayerBounceReaction);

                entity.MustDie.Add(new FuncCondition(() => entity.BounceCount.Value + 1 == 0), 5);

                entity
                    .AddSystem(new BounceDetectorSystem())
                    .AddSystem(new ReflectMovementDirectionOnBounceSystem())
                    .AddSystem(new ReflectRotationDirectionOnBounceSystem())
                    .AddSystem(new BounceCountDecreaseSystem());
            }
        }

        public void Dispose()
        {
            _entitiesLifeContext.Added -= OnCreaturesAdded;
        }
    }
}
