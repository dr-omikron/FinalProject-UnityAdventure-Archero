using _Archero.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Gameplay Context Registrations");
        }
    }
}
