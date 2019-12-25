using Zenject;
using Middleware;

public class MasterInstaller : MonoInstaller
{
    //Set up all bindings for Zenject dependency injection
    public override void InstallBindings()
    {
        //Add Container to SignalBusInstaller
        SignalBusInstaller.Install(Container);

        //Declare Signals
        Container.DeclareSignal<LanguageChangedSignal>();
        Container.DeclareSignal<GamePausedChangedSignal>().OptionalSubscriber();
        Container.DeclareSignal<ActiveSceneChangedSignal>();
        Container.DeclareSignal<SettingsShouldShowChangedSignal>().OptionalSubscriber();
        Container.DeclareSignal<UserRegistrationSucceededSignal>().OptionalSubscriber();
        Container.DeclareSignal<UserRegistrationFailedSignal>().OptionalSubscriber();
        Container.DeclareSignal<UserLoginSucceededSignal>().OptionalSubscriber();
        Container.DeclareSignal<UserLoginFailedSignal>().OptionalSubscriber();

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
        Container.Bind<PlayFabManager>()
            .AsSingle()
            .Lazy();

        //Bind Factories
        Container.Bind<SetGamePausedCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<InitializeLocationCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<InitializeSettingsCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<SaveSettingsCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<LoadSceneCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<RegisterUserCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<LoginUserCommandFactory>()
            .AsSingle()
            .Lazy();

        //Bind Signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<SettingsContainer>(settingsContainer => settingsContainer.SetLanguage)
            .FromResolve();
        Container.BindSignal<ActiveSceneChangedSignal>()
            .ToMethod<IInputManager>(iM => iM.OnActiveSceneChanged)
            .FromResolve();
    }
}