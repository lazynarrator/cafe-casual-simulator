using Leopotam.Ecs;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
    private EcsFilter<InputDirection, Movable, AnimatedCharacter, PlayerTag> playerFilter;
    
    private StaticData staticData;
    private SceneData sceneData;
    
    public void Run()
    {
        foreach (var i in playerFilter)
        {
            ref InputDirection input = ref playerFilter.Get1(i);
            ref Movable move = ref playerFilter.Get2(i);
            ref AnimatedCharacter anim = ref playerFilter.Get3(i);
            
            Vector3 targetVelocity = new Vector3(input.direction.x * move.moveSpeed,
                move.moveRigidbody.velocity.y, input.direction.z * move.moveSpeed);
            move.moveRigidbody.velocity = Vector3.SmoothDamp(move.moveRigidbody.velocity,
                targetVelocity, ref staticData.zeroVelocity, staticData.moveSmoothing);
            
            if (Mathf.Abs(input.direction.x) > staticData.animStartDelta || Mathf.Abs(input.direction.z) > staticData.animStartDelta)
            {
                move.moveTransform.forward = input.direction;
                anim.animator.SetBool(anim.animStateName, true);
            }
            else
            {
                anim.animator.SetBool(anim.animStateName, false);
            }
        }
    }
}