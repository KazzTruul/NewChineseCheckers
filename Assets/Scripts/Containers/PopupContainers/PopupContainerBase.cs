using UnityEngine;

public abstract class PopupContainerBase : MonoBehaviour
{
    public void DestroyPopup()
    {
        Destroy(gameObject);
    }
}