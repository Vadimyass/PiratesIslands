using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "IslandInstaller", menuName = "Installers/IslandInstaller")]
public class IslandInstaller : ScriptableObjectInstaller<IslandInstaller>
{
    [SerializeField] private PlankConfig _plankConfig;
    [SerializeField] private PlankPool _plankPool;
    [SerializeField] private IslandConfig _islandConfig;
    [SerializeField] private IslandPool _islandPool;
    public override void InstallBindings()
    {
        Container
            .Bind<IslandFactory>()
            .FromNew()
            .AsSingle()
            .WithArguments(_islandConfig)
            .NonLazy();
        Container
            .Bind<IslandPool>()
            .FromComponentInNewPrefab(_islandPool)
            .AsSingle();
        Container
            .Bind<PlankFactory>()
            .FromNew()
            .AsSingle()
            .WithArguments(_plankConfig)
            .NonLazy();
        Container
            .Bind<PlankPool>()
            .FromComponentInNewPrefab(_plankPool)
            .AsSingle();
    }
}