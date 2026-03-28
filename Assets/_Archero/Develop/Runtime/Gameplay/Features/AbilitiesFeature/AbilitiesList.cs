using System;
using System.Collections.Generic;

namespace _Archero.Develop.Runtime.Gameplay.Features.AbilitiesFeature
{
    public class AbilitiesList
    {
        public Action<Ability> Added;
        private readonly List<Ability> _elements = new List<Ability>();

        public IReadOnlyList<Ability> Elements => _elements;

        public virtual void Add(Ability ability)
        {
            _elements.Add(ability);
            Added?.Invoke(ability);
        }
    }
}
