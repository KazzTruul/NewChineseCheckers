using Zenject;

public class MainMenuUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainMenuContainer>()
            .FromComponentOnRoot();
        Container.BindInterfacesTo<MainMenuContainer>()
            .FromComponentOnRoot();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<ILocalizable>(localizable => localizable.OnLanguageChanged)
            .FromResolve();
    }
}