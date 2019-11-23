using UnityEngine;
using Zenject;
using System.Linq;

public class SinglePlayerSceneInstaller : MonoInstaller
{
    [Inject]
    private ICommandDispatcher _commandDispatcher;
    [Inject]
    private DiContainer _diContainer;

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
        Container.Bind<SpawnBoardCommand>()
            .AsSingle()
            .Lazy();

        //Bind Signals
        Container.BindSignal<TileClickedSignal>()
            .ToMethod<IInputManager>(inputManager => inputManager.OnTileClicked)
            .FromResolve();
    }
}