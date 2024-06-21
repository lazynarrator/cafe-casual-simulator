using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Настройки сцены
/// </summary>
public class SceneData : MonoBehaviour
{
    public Camera mainCamera;
    
    [Header("Player")]
    public Transform playerSpawnPoint;
    public List<Transform> playerTransferPoint;
    
    [Header("Items")]
    [Tooltip("Данные итема")]
    public ItemData item;
    
    [Header("Generators")]
    [Tooltip("Данные производства итемов")]
    public GeneratorData generator;
    public Transform generatorSpawnPoint;
    public List<Transform> itemsSpawnPoint;

    [Header("Sell")]
    public float sellTime;
    public int maxOrderCount;
    public GameObject sellPrefab;
    public List<Transform> sellTransferPoint;
    
    [Header("Other transfers")]
    public GameObject shelvePrefab;
    public List<Transform> shelvesTransferPoint;
    public GameObject trashPrefab;
    
    [Header("UI")]
    public GameObject infoBubble;
    public Vector2 bubblePose;
    public TMP_Text moneyText;
}