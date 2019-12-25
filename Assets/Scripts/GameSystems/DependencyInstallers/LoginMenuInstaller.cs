using Zenject;
using Middleware;

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
        Container.BindSignal<UserRegistrationSucceededSignal>()
            .ToMethod<LoginMenuContainer>(container => container.OnRegisterUserSuccess)
            .FromResolve();
        Container.BindSignal<UserRegistrationFailedSignal>()
            .ToMethod<LoginMenuContainer>(container => container.OnRegisterUserFailure)
            .FromResolve();
        Container.BindSignal<UserLoginSucceededSignal>()
            .ToMethod<LoginMenuContainer>(container => container.OnLoginUserSuccess)
            .FromResolve();
        Container.BindSignal<UserLoginFailedSignal>()
            .ToMethod<LoginMenuContainer>(container => container.OnLoginUserFailure)
            .FromResolve();
    }
}