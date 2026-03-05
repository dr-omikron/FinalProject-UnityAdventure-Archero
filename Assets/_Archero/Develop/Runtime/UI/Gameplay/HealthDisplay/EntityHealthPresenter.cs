using System;
using System.Collections.Generic;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Archero.Develop.Runtime.UI.CommonViews;
using _Archero.Develop.Runtime.UI.Core;
using _Archero.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Archero.Develop.Runtime.UI.Gameplay.HealthDisplay
{
    public class EntityHealthPresenter : IPresenter
    {
        private readonly BarWithText _bar;
        private readonly Entity _entity;
        private ReactiveVariable<Teams> _team;
        private ReactiveVariable<float> _health;
        private ReactiveVariable<float> _maxHealth;

        private readonly List<IDisposable> _disposables =  new List<IDisposable>();

        public EntityHealthPresenter(Entity entity, BarWithText bar)
        {
            _entity = entity;
            _bar = bar;
        }

        public BarWithText Bar => _bar;

        public void Initialize()
        {
            _health = _entity.CurrentHealth;
            _maxHealth = _entity.MaxHealth;
            _team = _entity.Team;

            _disposables.Add(_health.Subscribe(OnHeathChanged));
            _disposables.Add(_maxHealth.Subscribe(OnMaxHeathChanged));
            _disposables.Add(_team.Subscribe(OnTeamChanged));

            UpdateHealth();
            UpdateFillerColorBy(_team.Value);
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
        }

        private void OnTeamChanged(Teams oldValue, Teams newValue) => UpdateFillerColorBy(newValue);
        private void OnMaxHeathChanged(float oldValue, float newValue) => UpdateHealth();
        private void OnHeathChanged(float oldValue, float newValue) => UpdateHealth();

        private void UpdateHealth()
        {
            _bar.UpdateText(_health.Value.ToString("0"));
            _bar.UpdateSlider(_health.Value / _maxHealth.Value);
        }

        private void UpdateFillerColorBy(Teams team)
        {
            if(team == Teams.MainHero)
                _bar.SetFillerColor(Color.green);
            else if (team == Teams.Enemies)
                _bar.SetFillerColor(Color.red);
        }
    }
}
