using System;
using _Archero.Develop.Runtime.Utilities.Reactive;

namespace _Archero.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public interface IStage : IDisposable
    {
        IReadOnlyEvent Completed { get; }
        
        void Start();
        void Update(float deltaTime);
        void Cleanup();
    }
}
