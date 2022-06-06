using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "IslandInstaller", menuName = "Installers/IslandInstaller")]
public class IslandInstaller : ScriptableObjectInstaller<IslandInstaller>
{
    [SerializeField] private PlankConfig _plankConfig;
    [SerializeField] private PlankPool _plankPool;
    public override void InstallBindings()
    {
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