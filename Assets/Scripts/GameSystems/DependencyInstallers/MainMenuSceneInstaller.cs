using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ChangeLanguageCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<ChangeVolumeCommandFactory>()
            .AsSingle()
            .Lazy();
    }
}