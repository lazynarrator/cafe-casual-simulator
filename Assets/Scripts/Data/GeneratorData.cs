using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/" + nameof(GeneratorData))]
public class GeneratorData : ScriptableObject
{
    public GameObject prefab;
    public float produceTime;
    public int maxItems;
}