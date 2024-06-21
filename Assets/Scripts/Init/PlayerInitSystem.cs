using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    
    public void Init()
    {
        EcsEntity playerEntity = world.NewEntity();
        
        ref var playerTag = ref playerEntity.Get<PlayerTag>();
        ref var playerMove = ref playerEntity.Get<Movable>();
        ref var playerAnim = ref playerEntity.Get<AnimatedCharacter>();
        ref var playerInput = ref playerEntity.Get<InputDirection>();
        ref var playerTransfer = ref playerEntity.Get<Transfer>();
        
        GameObject playerGO = Object.Instantiate(staticData.playerPrefab, sceneData.playerSpawnPoint.position, Quaternion.identity);
        playerMove.moveTransform = playerGO.transform;
        playerMove.moveRigidbody = playerGO.GetComponent<Rigidbody>();
        playerMove.moveSpeed = staticData.moveSpeed;
        playerAnim.animator = playerGO.GetComponentInChildren<Animator>();
        playerAnim.animStateName = staticData.animStateName;
        playerTransfer.maxItems = staticData.playerCapacity;
        playerTransfer.items = new Stack<Item>();
        playerTransfer.prefab = playerGO;
        playerTransfer.transferPoint = sceneData.playerTransferPoint;
        playerTransfer.giver = true;
        playerTransfer.taker = true;
        
        foreach (var points in sceneData.playerTransferPoint)
        {
            points.SetParent(playerGO.transform);
        }
    }
}