using Zenject;

public class DependencyInitializer : MonoInstaller
{
    //Set up all bindings for Zenject dependency injection
    public override void InstallBindings()
    {
        //Add Container to SignalBusInstaller
        SignalBusInstaller.Install(Container);

        //Declare signals
        Container.DeclareSignal<LanguageChangedSignal>();

        //Set up bindings
        Container.Bind<SettingsContainer>()
            .AsSingle()
            .Lazy();
        Container.Bind<ILocalizationManager>()
            .To<LocalizationManager>()
            .AsSingle()
            .Lazy();
        Container.Bind<ICommandHandler>()
            .To<CommandHandler>()
            .AsSingle()
            .Lazy();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<SettingsContainer>(settingsContainer => settingsContainer.SetLanguage)
            .FromResolve();
    }
}
