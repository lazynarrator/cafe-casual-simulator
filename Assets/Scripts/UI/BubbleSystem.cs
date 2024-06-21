using Leopotam.Ecs;
using UnityEngine;

public class BubbleSystem : IEcsRunSystem
{
    private EcsFilter<Bubble> bubbleFilter;
    private EcsFilter<Transfer, PlayerTag> playerFilter;
    private EcsFilter<Sell> sellFilter;
    
    public void Run()
    {
        foreach (int i in bubbleFilter)
        {
            ref Bubble bubble = ref bubbleFilter.Get1(i);
            
            if (bubble.isPlayerBubble)
            {
                ref Transfer transfer = ref playerFilter.Get1(i);
                
                if (transfer.items.Count < 1)
                {
                    bubble.GO.SetActive(false);
                }
                else
                {
                    bubble.GO.SetActive(true);
                    bubble.tmp.text = transfer.items.Count.ToString();
                    bubble.GO.transform.position = new Vector3(
                        transfer.prefab.transform.position.x,
                        transfer.prefab.transform.position.y,
                        transfer.prefab.transform.position.z);
                }
            }
            else
            {
                ref Sell sell = ref sellFilter.Get1(i);
                bubble.tmp.text = sell.currentOrderCount.ToString();
            }
        }
    }
}