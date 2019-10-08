using UnityEngine;

public class InputManager : IInputManager
{
    private Pawn _selectedPawn;

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                var clickable = hit.transform.GetComponent<IClickable>();

                if (clickable == null)
                    return;

                clickable.OnClick();
            }
        }
    }

    public void OnTileClicked(TileClickedSignal signal)
    {

    }

    public void OnPawnClicked(PawnClickedSignal signal)
    {

    }
}