using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] public AssetProvider _assetProvider;
    public override void InstallBindings()
    {
        Container.Bind().FromInstance(_assetProvider).AsSingle();
    }
}
