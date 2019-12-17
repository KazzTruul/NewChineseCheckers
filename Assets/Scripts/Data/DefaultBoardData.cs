public class DefaultBoardData : BoardData
{
    public override SpawnBoardStrategy Strategy { get => _strategy; }

    private SpawnBoardStrategy _strategy = new DefaultSpawnBoardStrategy();

    public override int[] RowLengths { get; } = { 1, 2, 3, 4, 13, 12, 11, 10, 9, 10, 11, 12, 13, 4, 3, 2, 1 };

    public override (BoardColor color, (int row, (int startIndex, int finalIndex) rowIndeces)[] positions)[] TileColors { get; } =
    {
        (BoardColor.White, new (int, (int, int))[]
        {
            (4, (4, 8)),
            (5, (3, 8)),
            (6, (2, 8)),
            (7, (1, 8)),
            (8, (0, 8)),
            (9, (1, 8)),
            (10, (2, 8)),
            (11, (3, 8)),
            (12, (4, 8))
        }),
        (BoardColor.Yellow, new (int, (int, int))[]
        {
            (13, (0, 3)),
            (14, (0, 2)),
            (15, (0, 1)),
            (16, (0, 0))
        }),
        (BoardColor.Blue, new (int, (int, int))[]
        {
            (9, (0, 0)),
            (10, (0, 1)),
            (11, (0, 2)),
            (12, (0, 3))
        }),
        (BoardColor.Green, new (int, (int, int))[]
        {
            (4, (0, 3)),
            (5, (0, 2)),
            (6, (0, 1)),
            (7, (0, 0))
        }),
        (BoardColor.Red, new (int, (int, int))[]
        {
            (0, (0, 0)),
            (1, (0, 1)),
            (2, (0, 2)),
            (3, (0, 3))
        }),
        (BoardColor.Magenta, new (int, (int, int))[]
        {
            (4, (9, 12)),
            (5, (9, 11)),
            (6, (9, 10)),
            (7, (9, 9))
        }),
        (BoardColor.Black, new (int, (int, int))[]
        {
            (9, (9, 9)),
            (10, (9, 10)),
            (11, (9, 11)),
            (12, (9, 12))
        })
    };
}