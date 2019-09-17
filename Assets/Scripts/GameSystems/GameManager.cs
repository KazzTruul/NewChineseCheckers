using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ILocalizationManager _localizationManager;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _localizationManager = new LocalizationManager();
    }
}
