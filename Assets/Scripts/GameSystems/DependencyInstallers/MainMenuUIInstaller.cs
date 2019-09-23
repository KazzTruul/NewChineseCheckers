using Zenject;

public class MainMenuUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Declare signals
        //Container.DeclareSignal<LanguageChangedSignal>();

        //Set up bindings
        Container.Bind<ILocalizationManager>()
            .To<LocalizationManager>()
            .AsCached()
            .Lazy();
        
        Container.Bind<ILocalizable>()
            .To<MainMenuContainer>()
            .FromComponentOnRoot();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<ILocalizable>(localizable => localizable.OnLanguageChanged)
            .FromResolve();
    }
}