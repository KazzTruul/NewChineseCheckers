using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SetAutoSaveEnabledCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<ChangeLanguageCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<ChangeVolumeCommandFactory>()
            .AsSingle()
            .Lazy();
    }
}