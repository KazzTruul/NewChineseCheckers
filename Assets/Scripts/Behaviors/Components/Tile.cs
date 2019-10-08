using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class Tile : MonoBehaviour, IClickable
{
    private Pawn _currentPawn;

    private (int x, int y) _coordinates;

    private readonly Dictionary<Direction, Tile> _neighbors = new Dictionary<Direction, Tile>();

    private IInputManager _inputManager;

    private SignalBus _signalBus;

    public Pawn CurrentPawn => _currentPawn;

    public (int x, int y) Coordinates => _coordinates;

    public Dictionary<Direction, Tile> Neighbors => _neighbors;

    public void OnClick()
    {
        _signalBus.Fire(new TileClickedSignal() { Tile = this });
    }

    public void Initialize((int x, int y) coordinates, IInputManager inputManager, SignalBus signalBus)
    {
        _coordinates = coordinates;
        _inputManager = inputManager;
        _signalBus = signalBus;
    }

    public void AddNeighbor(Tile tile, Direction dir)
    {
        _neighbors.Add(dir, tile);
    }
}
