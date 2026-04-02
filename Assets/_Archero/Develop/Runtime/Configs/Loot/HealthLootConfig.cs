using UnityEngine;

namespace _Archero.Develop.Runtime.Configs.Loot
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Loot/HealthLootConfig", fileName = "HealthLootConfig")]
    public class HealthLootConfig : LootConfig
    {
        [field: SerializeField] public float Health { get; private set; }
    }
}
