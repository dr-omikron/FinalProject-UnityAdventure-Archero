using System.Collections.Generic;
using System.Linq;
using _Archero.Develop.Runtime.Configs.Loot;
using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.LootFeature
{
    public class DropLootService
    {
        private readonly LootListConfig _lootListConfig;
        private readonly LootFactory _lootFactory;

        public DropLootService(LootListConfig lootListConfig, LootFactory lootFactory)
        {
            _lootListConfig = lootListConfig;
            _lootFactory = lootFactory;
        }

        public void DropLootFor(Entity entity)
        {
            Transform entityTransform = entity.Transform;

            List<ExperienceLootConfig> expConfig = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(ExperienceLootConfig))
                .Cast<ExperienceLootConfig>()
                .ToList();

            if (expConfig.Count > 0)
                DropExp(entityTransform.position, expConfig[Random.Range(0, expConfig.Count)]);

            DropCoins(entityTransform.position);
            DropHealth(entityTransform.position);
        }

        private void DropExp(Vector3 position, ExperienceLootConfig experienceLootConfig)
        {
            int expInOnePotion = 300;

            if (experienceLootConfig.Experience < expInOnePotion)
            {
                _lootFactory.CreateExperienceLoot(experienceLootConfig.PrefabPath, position, experienceLootConfig.Experience);
            }
            else
            {
                int restOfExp = experienceLootConfig.Experience % expInOnePotion;
                int potionNumbers = (experienceLootConfig.Experience - restOfExp) / expInOnePotion;

                for (int i = 0; i < potionNumbers; i++)
                {
                    _lootFactory.CreateExperienceLoot(experienceLootConfig.PrefabPath, position, expInOnePotion);
                }
            }
        }

        private void DropHealth(Vector3 entityTransformPosition)
        {
            List<HealthLootConfig> healthConfigs = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(HealthLootConfig))
                .Cast<HealthLootConfig>()
                .ToList();

            if (healthConfigs.Count > 0 && Random.Range(0, 100) > 50)
            {
                HealthLootConfig healthLootConfig = healthConfigs[Random.Range(0, healthConfigs.Count)];
                _lootFactory.CreateHealthLoot(healthLootConfig.PrefabPath, entityTransformPosition, healthLootConfig.Health);
            }
        }

        private void DropCoins(Vector3 entityTransformPosition)
        {
            List<CoinsLootConfig> coinsConfigs = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(CoinsLootConfig))
                .Cast<CoinsLootConfig>()
                .ToList();

            if (coinsConfigs.Count > 0 && Random.Range(0, 100) > 50)
            {
                CoinsLootConfig coinsLootConfigs = coinsConfigs[Random.Range(0, coinsConfigs.Count)];
                _lootFactory.CreateCoinsLoot(coinsLootConfigs.PrefabPath, entityTransformPosition, coinsLootConfigs.Coins);
            }
        }
    }
}
