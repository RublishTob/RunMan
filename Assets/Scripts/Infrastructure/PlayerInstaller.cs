using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private AvatarFromPhoto _avatarFromPhoto;
    public override void InstallBindings()
    {
        Container.Bind().FromInstance(_avatarFromPhoto).AsSingle();
    }
}
