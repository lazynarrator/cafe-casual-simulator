using Leopotam.Ecs;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    [SerializeField] private StaticData staticData;
    [SerializeField] private SceneData sceneData;
    [SerializeField] private UI ui;
    
    private EcsWorld world;
    private EcsSystems updateSystems;
    private EcsSystems fixedUpdateSystems;
    
    private void Start()
    {
        world = new EcsWorld();
        updateSystems = new EcsSystems(world);
        fixedUpdateSystems = new EcsSystems(world);
        
        updateSystems
            .Add(new PlayerInitSystem())
            .Add(new InputSystem())
            .Add(new ProductionInitSystem())
            .Add(new GeneratorSystem())
            .Add(new TransferSystem())
            .Add(new SellSystem())
            .Add(new UIInitSystem())
            .Add(new BubbleSystem())
            .Inject(staticData)
            .Inject(sceneData)
            .Inject(ui)
            .Init();
        
        fixedUpdateSystems
            .Add(new PlayerMoveSystem())
            .Add(new CameraFollowSystem())
            .Inject(staticData)
            .Inject(sceneData)
            .Inject(ui)
            .Init();
    }
 
    private void Update()
    {
        updateSystems?.Run();
    }
    
    private void FixedUpdate()
    {
        fixedUpdateSystems?.Run();
    }
 
    private void OnDestroy()
    {
        updateSystems?.Destroy();
        updateSystems = null;
        fixedUpdateSystems?.Destroy();
        fixedUpdateSystems = null;
        world?.Destroy();
        world = null;
    }
}