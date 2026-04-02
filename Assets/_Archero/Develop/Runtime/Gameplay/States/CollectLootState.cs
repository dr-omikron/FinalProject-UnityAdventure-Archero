using _Archero.Develop.Runtime.Gameplay.Features.LootFeature;
using _Archero.Develop.Runtime.Gameplay.Features.MainHero;
using _Archero.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.States
{
    public class CollectLootState : State, IUpdatableState
    {
        private readonly LootPullingService _pullingService;
        private readonly MainHeroHolderService _heroHolderService;

        public CollectLootState(LootPullingService pullingService, MainHeroHolderService heroHolderService)
        {
            _pullingService = pullingService;
            _heroHolderService = heroHolderService;
        }

        public override void Enter()
        {
            base.Enter();

            _pullingService.PullTo(_heroHolderService.MainHero);
        }

        public override void Exit()
        {
            base.Exit();

            _pullingService.Reset();
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}
