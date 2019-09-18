using Zenject;

public class DependencyInitializer : MonoInstaller
{
    //Set up all bindings for Zenject dependency injection
    public override void InstallBindings()
    {
        Container.Bind<ILocalizationManager>()
            .To<LocalizationManager>()
            .AsSingle()
            .Lazy();
    }
}
