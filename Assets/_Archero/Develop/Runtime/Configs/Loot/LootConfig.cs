using UnityEngine;

namespace _Archero.Develop.Runtime.Configs.Loot
{
    public abstract class LootConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string PrefabPath { get; private set; }
    }
}
