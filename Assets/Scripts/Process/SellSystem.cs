using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;

public class SellSystem : IEcsRunSystem
{
    private EcsWorld world;
    private EcsFilter<Sell> sellFilter;
    
    public void Run()
    {
        foreach (int i in sellFilter)
        {
            ref Sell sell = ref sellFilter.Get1(i);
            ref Bubble bubble = ref sell.bubbleEntity.Get<Bubble>();
            
            switch (sell.state)
            {
                case Sell.State.NewOrder:
                    sell.currentOrderCount = Random.Range(1, sell.maxOrderCount + 1);
                    sell.state = Sell.State.Waiting;
                    break;
                
                case Sell.State.Waiting:
                    if (sell.sellMarker && sell.source.items.Count >= sell.currentOrderCount)
                    {
                        sell.state = Sell.State.Selling;
                        sell.lastSellTime = Time.time;
                    }
                    break;
                
                case Sell.State.Selling:
                    if (sell.source.items.Count >= sell.currentOrderCount)
                    {
                        if (sell.sellMarker)
                        {
                            if (Time.time - sell.lastSellTime < sell.sellTime)
                            {
                                float t = (Time.time - sell.lastSellTime) / sell.sellTime;
                                bubble.bgImage.color = Color.Lerp(Color.white, Color.green, t);
                            }
                            else
                            {
                                sell.state = Sell.State.Done;
                    
                                for (int k = 0; k < sell.currentOrderCount; k++)
                                {
                                    Item item = sell.source.items.Pop();
                                    Object.Destroy(item.prefab);
                                    
                                    ref Cash cash = ref sell.cashEntity.Get<Cash>();
                                    cash.sum = cash.sum + item.price;
                                    cash.text.text = cash.sum.ToString();
                                    
                                    item.itemEntity.Destroy();
                                }
                            }
                        }
                        else if (Time.time - sell.lastSellTime < sell.sellTime)
                        {
                            sell.state = Sell.State.Waiting;
                            bubble.bgImage.color = Color.white;
                        }
                    }
                    break;
                
                case Sell.State.Done:
                    bubble.bgImage.color = Color.white;
                    sell.state = Sell.State.NewOrder;
                    break;
            }
        }
    }
}