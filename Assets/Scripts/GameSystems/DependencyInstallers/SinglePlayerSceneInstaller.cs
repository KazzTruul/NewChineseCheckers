using Zenject;

public class SinglePlayerSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Declare Signals
        Container.DeclareSignal<TileClickedSignal>();

        //Set up Bindings
        Container.Bind<TileFactory>()
            .AsSingle()
            .Lazy();
        Container.Bind<BoardData>()
            .To<DefaultBoardData>()
            .AsSingle()
            .Lazy();
        Container.Bind<SpawnBoardCommandFactory>()
            .AsSingle()
            .Lazy();

        //Bind Signals
        Container.BindSignal<TileClickedSignal>()
            .ToMethod<IInputManager>(inputManager => inputManager.OnTileClicked)
            .FromResolve();
        Container.BindSignal<ActiveSceneChangedSignal>()
            .ToMethod<SinglePlayerSceneInitializer>(s => s.OnActiveSceneChanged)
            .FromNew();
    }
}