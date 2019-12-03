using Zenject;

public class MainMenuUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<MainMenuContainer>()
            .FromComponentOnRoot();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<ILocalizable>(localizable => localizable.OnLanguageChanged)
            .FromResolve();
    }
}