using UnityEngine;

/// <summary>
/// Предустановленные данные и настройки
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Data/" + nameof(StaticData))]
public class StaticData : ScriptableObject
{
    [Header("Player")]
    [Tooltip("Префаб игрока")]
    public GameObject playerPrefab;
    [Tooltip("Количество итемов которое может перенести игрок")]
    public int playerCapacity = 3;
    [Tooltip("Скорость передвижения игрока")]
    public float moveSpeed = 0.2f;
    [Tooltip("Коэффициент сглаживания движения")]
    public float moveSmoothing = 0.05f;
    [Tooltip("Нулевое ускорение движения"), HideInInspector]
    public Vector3 zeroVelocity = Vector3.zero;
    [Tooltip("Имя аниматора для смены состояний")]
    public string animStateName = "Static_b";
    [Tooltip("Минимальное смещение для начала анимации")]
    public float animStartDelta = 0.1f;
    
    [Header("Camera")]
    [Tooltip("Смещение камеры относительно игрока")]
    public Vector3 cameraOffset;
}