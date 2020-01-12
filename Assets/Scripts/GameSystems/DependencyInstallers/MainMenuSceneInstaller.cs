using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SettingsMenuContainer>()
            .FromComponentInHierarchy()
            .AsSingle()
            .Lazy();
        Container.Bind<ChangeLanguageCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<ChangeVolumeCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<ShowSettingsCommandFactory>()
            .AsSingle()
            .Lazy();
    }
}