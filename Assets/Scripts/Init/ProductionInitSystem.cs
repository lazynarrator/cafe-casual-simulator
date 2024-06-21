using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class ProductionInitSystem : IEcsInitSystem
{
    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    
    public void Init()
    {
        EcsEntity generatorEntity = world.NewEntity();
        ref var generator = ref generatorEntity.Get<Generator>();
        ref var generatorTransfer = ref generatorEntity.Get<Transfer>();
        GameObject generatorGO = Object.Instantiate(sceneData.generator.prefab,
            sceneData.generatorSpawnPoint.position, sceneData.generatorSpawnPoint.rotation);
        generator.produceTime = sceneData.generator.produceTime;
        generator.lastSpawnTime = Time.time;
        generatorTransfer.items = new Stack<Item>();
        generatorTransfer.prefab = generatorGO;
        generatorTransfer.transferPoint = sceneData.itemsSpawnPoint;
        generatorTransfer.maxItems = sceneData.generator.maxItems;
        generatorTransfer.giver = true;
        generatorTransfer.taker = false;
        CollideView generatorView = generatorGO.GetComponent<CollideView>();
        generatorView.Init(world);
        
        EcsEntity shelvesEntity = world.NewEntity();
        ref var shelvesTransfer = ref shelvesEntity.Get<Transfer>();
        shelvesTransfer.items = new Stack<Item>();
        shelvesTransfer.prefab = sceneData.shelvePrefab;
        shelvesTransfer.transferPoint = sceneData.shelvesTransferPoint;
        shelvesTransfer.maxItems = sceneData.shelvesTransferPoint.Count;
        shelvesTransfer.giver = false;
        shelvesTransfer.taker = true;
        CollideView shelvesView = shelvesTransfer.prefab.GetComponent<CollideView>();
        shelvesView.Init(world);
        
        EcsEntity sellEntity = world.NewEntity();
        ref var sell = ref sellEntity.Get<Sell>();
        sell.prefab = sceneData.sellPrefab;
        sell.source = shelvesTransfer;
        sell.state = Sell.State.NewOrder;
        sell.sellTime = sceneData.sellTime;
        sell.maxOrderCount = sceneData.maxOrderCount;
        CollideView sellView = sell.prefab.GetComponent<CollideView>();
        sellView.Init(world);
    }
}