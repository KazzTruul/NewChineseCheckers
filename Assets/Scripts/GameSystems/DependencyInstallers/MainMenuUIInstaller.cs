using Zenject;

public class MainMenuUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Declare signals
        Container.DeclareSignal<LanguageChangedSignal>();

        //Set up bindings
        Container.Bind<ILocalizationManager>()
            .To<LocalizationManager>()
            .AsSingle()
            .Lazy();

        Container.Bind<MainMenuContainer>()
            .FromComponentOnRoot();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<MainMenuContainer>(mainMenuContainer => mainMenuContainer.OnLanguageUpdated)
            .FromResolve();
    }
}
public class OptionsMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Declare signals
        Container.DeclareSignal<LanguageChangedSignal>();

        //Set up bindings
        Container.Bind<ILocalizationManager>()
            .To<LocalizationManager>()
            .AsSingle()
            .Lazy();
               
        Container.Bind<SettingsMenuContainer>()
            .FromComponentOnRoot();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<SettingsMenuContainer>(settingsMenuContainer => settingsMenuContainer.OnLanguageUpdated)
            .FromResolve();
    }
}