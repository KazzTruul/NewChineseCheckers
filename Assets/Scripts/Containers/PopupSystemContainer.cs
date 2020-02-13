using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PopupSystemContainer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _popupPrefabs = new List<GameObject>();
    
    private List<IPopupFactory> _popupFactories;

    private PopupContainerBase _currentPopup;

    private RectTransform _activeCanvasTransform;

    private readonly Dictionary<Type, GameObject> _popups = new Dictionary<Type, GameObject>();

    [Inject]
    private void Initialize(List<IPopupFactory> popupFactories)
    {
        _popupFactories = popupFactories;

        foreach (var popupPrefab in _popupPrefabs)
        {
            var popupType = popupPrefab.GetComponent<IPopupFactory>()?.GetType();

            if (popupType == null)
            {
                throw new Exception($"NO POPUP COMPONENT FOUND ON {popupPrefab.name}!");
            }

            if (_popups.ContainsKey(popupType))
            {
                throw new Exception($"DUPLICATE PREFABS FOUND FOR POPUP OF TYPE {popupType}");
            }

            _popups.Add(popupType, popupPrefab);
        }
    }

    public void OnActiveSceneChanged(ActiveSceneChangedSignal signal)
    {
        //Set new canvas

        if (_currentPopup != null)
        {

        }
    }

    public void ShowPopup<T>() where T : PopupContainerBase
    {
        HidePopup();

        var currentPopupFactory = _popupFactories.FirstOrDefault(p => p.PopupType == typeof(T));

        if (currentPopupFactory == null)
        {
            throw new Exception($"NO FACTORY FOR POPUP TYPE {typeof(T).Name} FOUND!");
        }

        if (!_popups.ContainsKey(typeof(T)))
        {
            throw new Exception($"NO PREFAB FOUND FOR POPUP OF TYPE {typeof(T).Name}");
        }

        _currentPopup = currentPopupFactory.Create(_popups[typeof(T)]);
    }

    public void HidePopup()
    {
        if (_currentPopup != null)
        {
            _currentPopup.DestroyPopup();
        }
    }
}
