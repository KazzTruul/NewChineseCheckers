using Zenject;

public class SinglePlayerSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Add Container to SignalBusInstaller
        SignalBusInstaller.Install(Container);

        //Declare Signals
        Container.DeclareSignal<TileClickedSignal>();
        Container.DeclareSignal<PawnClickedSignal>();

        //Set up Bindings
        Container.Bind<TileFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<BoardData>()
            .To<DefaultBoardData>()
            .AsSingle()
            .Lazy();

        //Bind Signals
        Container.BindSignal<TileClickedSignal>()
            .ToMethod<IInputManager>(inputManager => inputManager.OnTileClicked)
            .FromResolve();
    }
}