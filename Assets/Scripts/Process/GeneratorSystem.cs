using Leopotam.Ecs;
using UnityEngine;

public class GeneratorSystem : IEcsRunSystem
{
    private EcsWorld world;
    private SceneData sceneData;
    private EcsFilter<Generator, Transfer> generatorFilter;
    
    public void Run()
    {
        foreach (int i in generatorFilter)
        {
            ref Generator generator = ref generatorFilter.Get1(i);
            ref Transfer transfer = ref generatorFilter.Get2(i);
            
            if (Time.time - generator.lastSpawnTime >= generator.produceTime
                && transfer.items.Count < transfer.maxItems)
            {
                EcsEntity itemEntity = world.NewEntity();
                ref Item item = ref itemEntity.Get<Item>();
                item.itemEntity = itemEntity;
                item.prefab = Object.Instantiate(sceneData.item.prefab,
                    transfer.transferPoint[transfer.items.Count].position,
                    transfer.transferPoint[transfer.items.Count].rotation);
                item.price = sceneData.item.price;
                
                transfer.items.Push(item);
                generator.lastSpawnTime = Time.time;
            }
        }
    }
}