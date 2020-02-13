using System.Collections.Generic;
using Zenject;

public class PopupSystemInstaller : MonoInstaller
{
    private readonly List<IPopupFactory> _popupFactories = new List<IPopupFactory>();

    public override void InstallBindings()
    {
        Container.BindSignal<ActiveSceneChangedSignal>()
            .ToMethod<PopupSystemContainer>(p => p.OnActiveSceneChanged);

        //var settingsMenuPopupFactory = new PopupFactory<SettingsMenuPopupContainer>();

        //_popupFactories.Add(settingsMenuPopupFactory);

        //Container.BindFactory<SettingsMenuPopupContainer, PopupFactory<SettingsMenuPopupContainer>>()
        //    .FromInstance(settingsMenuPopupFactory)
        //    .Lazy();


        // var x = Container.Bind<List<IPopupFactory>>()
        //    .FromInstance(_popupFactories)
        //    .AsSingle()
        //    .Lazy();
    }
}
