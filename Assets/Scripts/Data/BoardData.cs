public abstract class BoardData
{
    public abstract SpawnBoardStrategy Strategy { get; }
    public abstract int[] RowLengths { get; }
    public abstract (BoardColor color, (int row, (int startIndex, int finalIndex) rowIndeces)[] positions)[] TileColors { get; }
}