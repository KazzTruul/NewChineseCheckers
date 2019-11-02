public abstract class BoardData
{
    public abstract SpawnBoardStrategy SpawnBoardStrategy { get; protected set; }
    public abstract int[] RowLengths { get; }
    public abstract (BoardColor color, (int row, (int startIndex, int finalIndex) rowIndeces)[] positions)[] TileColors { get; }
}