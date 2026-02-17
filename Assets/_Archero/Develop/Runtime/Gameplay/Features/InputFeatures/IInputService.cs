using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.InputFeatures
{
    public interface IInputService
    {
        bool IsEnabled { get; set; }
        Vector3 Direction { get; }
    }
}
