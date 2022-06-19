using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private CharacterPool _characterPool;
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private PlayerManager _playerManager;
    public override void InstallBindings()
    {
        Container
            .Bind<NavMeshSurface>()
            .FromInstance(_surface)
            .AsSingle();
        Container
            .Bind<CameraController>()
            .FromNew()
            .AsSingle()
            .WithArguments(_camera)
            .NonLazy();
        Container
            .Bind<CharacterFactory>()
            .FromNew()
            .AsSingle()
            .WithArguments(_characterConfig);
        Container
            .Bind<CharacterPool>()
            .FromComponentInNewPrefab(_characterPool)
            .AsSingle();
        Container
            .Bind<PlayerManager>()
            .FromComponentInNewPrefab(_playerManager)
            .AsSingle();
    }
}