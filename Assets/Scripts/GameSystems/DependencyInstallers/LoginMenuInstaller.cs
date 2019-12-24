using Zenject;

public class LoginMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LoginMenuContainer>()
            .FromComponentOnRoot();
        Container.BindInterfacesTo<LoginMenuContainer>()
            .FromComponentOnRoot();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<ILocalizable>(localizable => localizable.OnLanguageChanged)
            .FromResolve();
    }
}