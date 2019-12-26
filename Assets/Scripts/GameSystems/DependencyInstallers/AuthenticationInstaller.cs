using Zenject;
using Middleware;

public class AuthenticationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AuthenticationContainer>()
            .FromComponentOnRoot();
        Container.BindInterfacesTo<AuthenticationContainer>()
            .FromComponentOnRoot();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<ILocalizable>(localizable => localizable.OnLanguageChanged)
            .FromResolve();
        Container.BindSignal<UserRegistrationSucceededSignal>()
            .ToMethod<AuthenticationContainer>(container => container.OnRegisterUserSuccess)
            .FromResolve();
        Container.BindSignal<UserRegistrationFailedSignal>()
            .ToMethod<AuthenticationContainer>(container => container.OnRegisterUserFailure)
            .FromResolve();
        Container.BindSignal<UserLoginSucceededSignal>()
            .ToMethod<AuthenticationContainer>(container => container.OnLoginUserSuccess)
            .FromResolve();
        Container.BindSignal<UserLoginFailedSignal>()
            .ToMethod<AuthenticationContainer>(container => container.OnLoginUserFailure)
            .FromResolve();
    }
}