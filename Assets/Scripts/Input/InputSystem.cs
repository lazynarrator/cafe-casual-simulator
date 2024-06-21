using Leopotam.Ecs;
using UnityEngine;

public class InputSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
{
    private UI ui;
    private EcsFilter<InputDirection> inputFilter;
    
    public void Init()
    {
        ui.joystick.OnSwipeProgress += OnSwipe;
        ui.joystick.OnSwipeStop += OnStop;
    }
    
    public void Destroy()
    {
        ui.joystick.OnSwipeProgress -= OnSwipe;
        ui.joystick.OnSwipeStop -= OnStop;
    }
    
    private void OnSwipe(Vector2 _direction)
    {
        foreach (var i in inputFilter)
        {
            ref InputDirection input = ref inputFilter.Get1(i);
            input.direction = new Vector3(_direction.x, input.direction.y, _direction.y);
        }
    }
    
    private void OnStop()
    {
        foreach (var i in inputFilter)
        {
            ref InputDirection input = ref inputFilter.Get1(i);
            input.direction = new Vector3(Vector3.zero.x, input.direction.y, Vector3.zero.z);
        }
    }
    
    public void Run()
    {
        
    }
}