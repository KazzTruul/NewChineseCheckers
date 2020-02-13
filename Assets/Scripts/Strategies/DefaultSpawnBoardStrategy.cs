using UnityEngine;

public class DefaultSpawnBoardStrategy : SpawnBoardStrategy
{
    protected override float TileOffset { get; } = 2.0f;

    private const float YPos = 0.0f;
    
    public override Tile[][] SpawnBoard(int[] rowLengths, TileFactory tileFactory)
    {
        var board = new Tile[rowLengths.Length][];

        for (var row = 0; row < rowLengths.Length; row++)
        {
            board[row] = new Tile[rowLengths[row]];

            var zPos = ((rowLengths.Length * TileOffset) / 2) - (TileOffset * row) - (TileOffset / 2.0f);

            for (var column = 0; column < rowLengths[row]; column++)
            {
                var tile = tileFactory.Create(column, row);

                var xPos = (-(rowLengths[row] * TileOffset) / 2) + (TileOffset * column) + (TileOffset / 2.0f);

                tile.transform.position = new Vector3(xPos, YPos, zPos);
                tile.transform.rotation = new Quaternion(-90.0f, 0.0f, 0.0f, tile.transform.rotation.w);

                board[row][column] = tile;
            }
        }

        ConnectBoard(board);

        return board;
    }

    protected override void ConnectBoard(Tile[][] board)
    {
        for (var row = 0; row < board.Length; row++)
        {
            var upperIndexOffset = row > 0 ? board[row].Length - board[row - 1].Length : 0;
            var lowerIndexOffset = row < board.Length - 1 ? board[row].Length - board[row + 1].Length : 0;

            for (var column = 0; column < board[row].Length; column++)
            {
                if (row > 0)
                {
                    var upperLeft = (-(upperIndexOffset + (upperIndexOffset % 2 == 0 ? 0 : 1)) / 2) + column;
                    var upperRight = upperLeft + 1;

                    if (upperLeft >= 0 && upperLeft < board[row - 1].Length)
                    {
                        board[row][column].AddNeighbor(board[row - 1][upperLeft], Direction.LeftUp);
                    }

                    if (upperRight >= 0 && upperRight < board[row - 1].Length)
                    {
                        board[row][column].AddNeighbor(board[row - 1][upperRight], Direction.RightUp);
                    }
                }

                if (row < board.Length - 1)
                {
                    var lowerLeft = (-(lowerIndexOffset + (lowerIndexOffset % 2 == 0 ? 0 : 1)) / 2) + column;
                    var lowerRight = lowerLeft + 1;

                    if (lowerLeft >= 0 && lowerLeft < board[row + 1].Length)
                    {
                        board[row][column].AddNeighbor(board[row + 1][lowerLeft], Direction.LeftDown);
                    }

                    if (lowerRight >= 0 && lowerRight < board[row + 1].Length)
                    {
                        board[row][column].AddNeighbor(board[row + 1][lowerRight], Direction.RightDown);
                    }
                }

                if (column > 0)
                {
                    board[row][column].AddNeighbor(board[row][column - 1], Direction.Left);
                }

                if (column < board[row].Length - 1)
                {
                    board[row][column].AddNeighbor(board[row][column + 1], Direction.Right);
                }
            }
        }
    }
}