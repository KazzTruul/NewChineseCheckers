using Zenject;
using UnityEngine.SceneManagement;

public class MasterSceneInstaller : MonoInstaller
{
    [Inject]
    private readonly ZenjectSceneLoader _zenjectSceneLoader;

    //Set up all bindings for Zenject dependency injection
    public override void InstallBindings()
    {
        //Add Container to SignalBusInstaller
        SignalBusInstaller.Install(Container);

        //Declare Signals
        Container.DeclareSignal<LanguageChangedSignal>();

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
        Container.Bind<InitializeLocationCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<InitializeSettingsCommandFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<ApplySettingsCommandFactory>()
            .AsSingle()
            .Lazy();

        //Bind Signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<SettingsContainer>(settingsContainer => settingsContainer.SetLanguage)
            .FromResolve();

        _zenjectSceneLoader.LoadScene(1, LoadSceneMode.Additive);
    }
}