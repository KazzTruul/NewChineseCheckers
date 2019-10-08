using UnityEngine;

public abstract class SpawnBoardStrategy
{
    protected readonly TileFactory _tileFactory;
    protected virtual float _tileOffset { get; }
    public abstract Tile[][] SpawnBoard(int[] rowLengths);
    protected abstract void ConnectBoard(Tile[][] board);

    public SpawnBoardStrategy(TileFactory tileFactory)
    {
        _tileFactory = tileFactory;
    }
}