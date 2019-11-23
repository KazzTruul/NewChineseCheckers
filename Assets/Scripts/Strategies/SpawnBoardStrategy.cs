using UnityEngine;

public abstract class SpawnBoardStrategy
{
    protected virtual float _tileOffset { get; }
    public abstract Tile[][] SpawnBoard(int[] rowLengths, TileFactory tileFactory);
    protected abstract void ConnectBoard(Tile[][] board);
}