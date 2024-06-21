using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public struct Transfer
{
    public GameObject prefab;
    public List<Transform> transferPoint;
    
    public EcsEntity fromEntity;
    public EcsEntity toEntity;
    
    public bool giver;
    public bool taker;
    
    public int maxItems;
    public Item currentItem;
    public Stack<Item> items;
    
    public bool transferMarker;
}