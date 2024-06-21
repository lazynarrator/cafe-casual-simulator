using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/" + nameof(ItemData))]
public class ItemData : ScriptableObject
{
    public GameObject prefab;
    public Sprite icon;
    public int price;
}