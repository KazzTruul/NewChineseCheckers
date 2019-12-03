using Zenject;

public interface IInputManager : ITickable
{
    bool GamePaused { get; }
    void OnTileClicked(TileClickedSignal signal);
    void OnPawnClicked(PawnClickedSignal signal);
    void OnActiveSceneChanged(ActiveSceneChangedSignal signal);
    void SetGamePaused(bool? pauseGame);
}