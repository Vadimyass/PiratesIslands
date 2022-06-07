using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "UIinstaller", menuName = "Installers/UIinstaller")]
public class UIinstaller : ScriptableObjectInstaller<UIinstaller>
{
    [SerializeField] private int _maxScore;
    [SerializeField] private Score _score;
    public override void InstallBindings()
    {
        Container
            .Bind<ScoreController>()
            .FromNew()
            .AsSingle()
            .WithArguments(_maxScore)
            .NonLazy();
        var score =
            Container.InstantiatePrefabForComponent<Score>(_score);
        Container
            .Bind<Score>()
            .FromInstance(score)
            .AsSingle();
    }
}