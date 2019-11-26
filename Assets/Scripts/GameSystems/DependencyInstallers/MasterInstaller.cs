using Zenject;

public class MasterInstaller : MonoInstaller
{
    //Set up all bindings for Zenject dependency injection
    public override void InstallBindings()
    {
        //Add Container to SignalBusInstaller
        SignalBusInstaller.Install(Container);

        //Declare Signals
        Container.DeclareSignal<LanguageChangedSignal>();
        Container.DeclareSignal<GamePausedChangedSignal>();
        Container.DeclareSignal<ActiveSceneChangedSignal>();

        //Set up Bindings
        Container.Bind<SettingsContainer>()
            .AsSingle()
            .Lazy();
        Container.Bind<ILocalizationManager>()
            .To<LocalizationManager>()
            .AsSingle()
            .Lazy();
        Container.Bind<ICommandDispatcher>()
            .To<CommandDispatcher>()
            .AsSingle()
            .Lazy();
        Container.Bind<IInputManager>()
            .To<InputManager>()
            .AsSingle()
            .Lazy();
        Container.Bind<ITickable>()
            .To<TickableManager>()
            .AsSingle()
            .Lazy();
        Container.Bind<CoroutineRunner>()
            .FromComponentOnRoot();

        //Bind Factories
        Container.Bind<InitializeLocationCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<InitializeSettingsCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<ApplySettingsCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<LoadSceneCommandFactory>()
            .AsSingle()
            .Lazy();

        //Bind Signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<SettingsContainer>(settingsContainer => settingsContainer.SetLanguage)
            .FromResolve();
    }
}