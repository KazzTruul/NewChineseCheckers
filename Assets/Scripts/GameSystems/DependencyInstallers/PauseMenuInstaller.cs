﻿using Zenject;

public class PauseMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Bind interfaces
        Container.BindInterfacesTo<PauseMenuContainer>()
            .FromComponentOnRoot();

        //Bind signals
        Container.BindSignal<LanguageChangedSignal>()
            .ToMethod<ILocalizable>(localizable => localizable.OnLanguageChanged)
            .FromResolve();
        Container.BindSignal<GamePausedChangedSignal>()
            .ToMethod<IPausable>(pausable => pausable.OnPausedAndUnpaused)
            .FromResolve();
    }
}