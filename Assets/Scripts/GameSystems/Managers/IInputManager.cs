using Zenject;

public interface IInputManager : ITickable
{
    bool GamePaused { get; }
    void OnTileClicked(TileClickedSignal signal);
    void OnPawnClicked(PawnClickedSignal signal);
    void OnActiveSceneChanged(ActiveSceneChangedSignal signal);
    void SetGamePaused(bool? pauseGame);
    void SetGameState(GameState newState);
    void RemoveGameState(GameState obsoleteState);
}