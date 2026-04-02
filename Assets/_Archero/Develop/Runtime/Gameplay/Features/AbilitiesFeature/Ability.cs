using System;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature
{
    public abstract class Ability
    {
        private ReactiveVariable<int> _currentLevel;

        protected Ability(string id, int currentLevel, int maxLevel)
        {
            ID = id;
            _currentLevel = new ReactiveVariable<int>(currentLevel);
            MaxLevel = maxLevel;
        }

        public string ID { get; }
        public int MaxLevel { get; }
        public IReadOnlyVariable<int> CurrentLevel => _currentLevel;

        public void AddLevel(int level)
        {
            int temp = _currentLevel.Value + level;

            if (temp > MaxLevel)
                throw new ArgumentException(nameof(level));

            _currentLevel.Value = temp;
        }

        public abstract void Activate();
    }
}
