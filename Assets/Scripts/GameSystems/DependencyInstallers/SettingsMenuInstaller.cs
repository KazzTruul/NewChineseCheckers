using Zenject;

public class SettingsMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<SettingsMenuContainer>()
            .FromComponentOnRoot();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<ILocalizable>(localizable => localizable.OnLanguageChanged)
            .FromResolve();
    }
}