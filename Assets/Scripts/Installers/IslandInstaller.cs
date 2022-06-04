using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "IslandInstaller", menuName = "Installers/IslandInstaller")]
public class IslandInstaller : ScriptableObjectInstaller<IslandInstaller>
{
    [SerializeField] private WoddenLog _logPrefab;
    [SerializeField] private Rigidbody _logRB;
    public override void InstallBindings()
    {
        Container
            .Bind<WoddenLog>()
            .FromInstance(_logPrefab)
            .AsSingle();
        Container
            .Bind<Rigidbody>()
            .FromInstance(_logRB)
            .AsSingle();
    }
}