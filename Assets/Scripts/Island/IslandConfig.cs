using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "IslandConfig", menuName = "Installers/IslandConfig")]
public class IslandConfig : ScriptableObjectInstaller<IslandConfig>
{
    public override void InstallBindings()
    {
    }
}