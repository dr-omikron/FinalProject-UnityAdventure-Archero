using System;
using _Archero.Develop.Runtime.Configs.Gameplay.Entities;
using UnityEngine;

namespace _Archero.Develop.Runtime.Configs.Gameplay.Stages
{
    [Serializable]

    public class EnemyItemConfig
    {
        [field: SerializeField] public Vector3 SpawnPosition { get; private set; }
        [field: SerializeField] public EntityConfig EnemyConfig { get; private set; }
    }
}
