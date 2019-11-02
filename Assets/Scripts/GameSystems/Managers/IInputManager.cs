using Zenject;

public interface IInputManager : ITickable
{
    void OnTileClicked(TileClickedSignal signal);
    void OnPawnClicked(PawnClickedSignal signal);
}