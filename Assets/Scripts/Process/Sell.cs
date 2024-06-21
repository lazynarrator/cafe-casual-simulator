using Leopotam.Ecs;
using UnityEngine;

public struct Sell
{
    public GameObject prefab;
    public int maxOrderCount;
    public int currentOrderCount;
    public float sellTime;
    public float lastSellTime;
    public bool sellMarker;
    
    public Transfer source;
    public EcsEntity bubbleEntity;
    public EcsEntity cashEntity;
    
    public State state;
    
    public enum State: byte
    {
        NewOrder,
        Waiting,
        Selling,
        Done
    }
}