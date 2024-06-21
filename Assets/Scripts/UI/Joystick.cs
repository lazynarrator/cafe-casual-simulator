using System;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField, Tooltip("Центральный круг джойстика")]
    private RectTransform circle;
    [SerializeField, Tooltip("Обводка джойстика")]
    private RectTransform outerCircle;
    [SerializeField, Tooltip("Максимальное отставание обводки джойстика")]
    private float maxGap = 40f;
    
    private Vector3 lastMousePose;
    private Vector3 lastOuterPose;
    
    public event Action<Vector2> OnSwipeProgress;
    public event Action OnSwipeStop;
    
    /// <summary>
    /// Получить обработанную позицию нажатия
    /// </summary>
    private Vector3 GetPress()
    {
        lastMousePose = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        return new Vector3(lastMousePose.x * Screen.width, lastMousePose.y * Screen.height);
    }
    
    /// <summary>
    /// Вкл/выкл джойстик
    /// </summary>
    private void JoystickActive(bool _isActive)
    {
        circle.gameObject.SetActive(_isActive);
        outerCircle.gameObject.SetActive(_isActive);
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastOuterPose = GetPress();
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector3 press = GetPress();
            Vector3 direction = Vector3.ClampMagnitude(press - lastOuterPose, maxGap);
            Vector3 gap = new Vector3(press.x - direction.x, press.y - direction.y);
            
            circle.position = press;
            outerCircle.position = gap;
            lastOuterPose = gap;
            
            if (!circle.gameObject.activeSelf)
                JoystickActive(!circle.gameObject.activeSelf);
            
            OnSwipeProgress?.Invoke(direction);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            JoystickActive(false);
            OnSwipeStop?.Invoke();
        }
    }
}