using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInitSystem : IEcsInitSystem
{
    private EcsWorld world;
    private StaticData staticData;
    private SceneData sceneData;
    
    private EcsFilter<Transfer, PlayerTag> playerFilter;
    private EcsFilter<Sell> sellFilter;
    
    public void Init()
    {
        EcsEntity playerBubbleEntity = world.NewEntity();
        ref Bubble playerBubble = ref playerBubbleEntity.Get<Bubble>();
        
        foreach (int i in playerFilter)
        {
            ref Transfer transfer = ref playerFilter.Get1(i);
            playerBubble.GO = Object.Instantiate(sceneData.infoBubble, transfer.prefab.transform.position,
                Quaternion.identity);
            var child = playerBubble.GO.GetComponentsInChildren<Canvas>()[0].gameObject;
            var pose = child.GetComponent<RectTransform>();
            pose.anchoredPosition = sceneData.bubblePose;
            playerBubble.tmp = child.GetComponentInChildren<TMP_Text>();
            playerBubble.isPlayerBubble = true;
        }
        
        EcsEntity cashEntity = world.NewEntity();
        ref Cash cash = ref cashEntity.Get<Cash>();
        cash.text = sceneData.moneyText;
        
        EcsEntity sellBubbleEntity = world.NewEntity();
        ref Bubble sellBubble = ref sellBubbleEntity.Get<Bubble>();
        
        foreach (int i in sellFilter)
        {
            ref Sell sell = ref sellFilter.Get1(i);
            sellBubble.GO = Object.Instantiate(sceneData.infoBubble, sell.prefab.transform.position,
                Quaternion.identity);
            var child = sellBubble.GO.GetComponentsInChildren<Canvas>()[0].gameObject;
            var pose = child.GetComponent<RectTransform>();
            pose.anchoredPosition = sceneData.bubblePose;
            sellBubble.tmp = child.GetComponentInChildren<TMP_Text>();
            sellBubble.bgImage = child.GetComponentsInChildren<Image>()[0];
            sell.bubbleEntity = sellBubbleEntity;
            sell.cashEntity = cashEntity;
        }
    }
}