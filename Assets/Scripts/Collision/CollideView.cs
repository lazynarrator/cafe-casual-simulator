using Leopotam.Ecs;
using UnityEngine;

public class CollideView : MonoBehaviour
{
    private EcsWorld world;
    private EcsFilter sellFilter;
    private EcsFilter transferFilter;
    private EcsEntity player;
    
    public void Init(EcsWorld _world)
    {
        world = _world;
        
        EcsFilter playerFilter = world.GetFilter(typeof(EcsFilter<Transfer, PlayerTag>));
        foreach (int i in playerFilter)
        {
            player = playerFilter.GetEntity(i);
        }
        
        sellFilter = world.GetFilter(typeof(EcsFilter<Sell>));
        transferFilter = world.GetFilter(typeof(EcsFilter<Transfer>));
    }
    
    private void OnCollisionStay(Collision _collision)
    {
        if (_collision.collider.CompareTag("Player") && world != null)
        {
            foreach (int i in transferFilter)
            {
                EcsEntity transferEntity = transferFilter.GetEntity(i);
                ref Transfer transter = ref transferEntity.Get<Transfer>();
                
                if (transter.prefab == gameObject)
                {
                    if (transter.giver)
                    {
                        transter.fromEntity = transferEntity;
                        transter.toEntity = player;
                    }
                    else if (transter.taker)
                    {
                        transter.fromEntity = player;
                        transter.toEntity = transferEntity;
                    }
                    transter.transferMarker = true;
                }
            }
            
            foreach (int i in sellFilter)
            {
                EcsEntity sellEntity = sellFilter.GetEntity(i);
                ref Sell sell = ref sellEntity.Get<Sell>();
                if (sell.prefab == gameObject)
                {
                    sell.sellMarker = true;
                }
            }
        }
    }

    private void OnCollisionExit(Collision _collision)
    {
        if (_collision.collider.CompareTag("Player") && world != null)
        {
            foreach (int i in transferFilter)
            {
                EcsEntity transferEntity = transferFilter.GetEntity(i);
                ref Transfer transter = ref transferEntity.Get<Transfer>();
                transter.transferMarker = false;
            }
            
            foreach (int i in sellFilter)
            {
                EcsEntity sellEntity = sellFilter.GetEntity(i);
                ref Sell sell = ref sellEntity.Get<Sell>();
                sell.sellMarker = false;
            }
        }
    }
}