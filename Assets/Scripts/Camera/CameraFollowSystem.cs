using Leopotam.Ecs;
using UnityEngine;

public class CameraFollowSystem : IEcsRunSystem
{
    private EcsFilter<Movable, PlayerTag> playerMoveFilter;
    private StaticData staticData;
    private SceneData sceneData;
    
    public void Run()
    {
        foreach (var i in playerMoveFilter)
        {
            ref Movable move = ref playerMoveFilter.Get1(i);
            
            sceneData.mainCamera.transform.position = new Vector3(
                move.moveTransform.position.x + staticData.cameraOffset.x,
                move.moveTransform.position.y + staticData.cameraOffset.y,
                move.moveTransform.position.z + staticData.cameraOffset.z);
        }
    }
}