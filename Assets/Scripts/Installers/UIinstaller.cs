using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "UIinstaller", menuName = "Installers/UIinstaller")]
public class UIinstaller : ScriptableObjectInstaller<UIinstaller>
{
    [SerializeField] private int _maxScore;
    [SerializeField] private ScoreView _scoreView;
    public override void InstallBindings()
    {
        Container
            .Bind<ScoreController>()
            .FromNew()
            .AsSingle()
            .WithArguments(_maxScore)
            .NonLazy();
        Container
            .BindInterfacesAndSelfTo<ScoreView>()
            .FromComponentInNewPrefab(_scoreView)
            .AsSingle();
    }
}