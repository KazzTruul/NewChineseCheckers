using UnityEngine;
using Zenject;
using System.Linq;

public class TileFactory
{
    private const string TileName = "Tile {0} {1}";
    private const string TileShaderName = "Unlit/Color";
    private const string TileShaderColorPropertyName = "_Color";
    
    [Inject]
    private readonly IInputManager _inputManager;
    [Inject]
    private readonly BoardData _boardData;
    [Inject]
    private readonly SignalBus _signalBus;

    public Tile Create(int x, int y)
    {
        var tileGO = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        tileGO.name = string.Format(TileName, y, x);
        tileGO.transform.localScale = new Vector3(1.0f, 0.1f, 1.0f);

        var tileColor = _boardData.TileColors
            .Where(indeces => indeces.positions
            .Where(coord => coord.row == y &&
            coord.rowIndeces.startIndex <= x &&
            coord.rowIndeces.finalIndex >= x)
            .Any())
            .FirstOrDefault()
            .color;

        var tileRenderer = tileGO.GetComponent<Renderer>();
        tileRenderer.material = new Material(Shader.Find(TileShaderName));
        tileRenderer.material.SetColor(TileShaderColorPropertyName, Constants.ColorConversions[tileColor]);

        var tileCollider = tileGO.GetComponent<Collider>();
        tileCollider.GetComponent<Collider>().isTrigger = true;

        var tile = tileGO.AddComponent<Tile>();
        tile.Initialize((y, x), _inputManager, _signalBus);
        return tile;
    }
}
