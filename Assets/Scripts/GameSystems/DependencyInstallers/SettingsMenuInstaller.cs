using Zenject;

public class SettingsMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SettingsMenuContainer>()
            .FromComponentOnRoot();
        Container.BindInterfacesTo<SettingsMenuContainer>()
            .FromComponentOnRoot();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<ILocalizable>(localizable => localizable.OnLanguageChanged)
            .FromResolve();
        Container.BindSignal<SettingsShouldShowChangedSignal>()
            .ToMethod<SettingsMenuContainer>(settingsMenuContainer => settingsMenuContainer.OnShowSettingsChanged)
            .FromResolve();
    }
}