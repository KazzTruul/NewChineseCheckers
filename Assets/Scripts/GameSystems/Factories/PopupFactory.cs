using System;
using UnityEngine;
using Zenject;

public class PopupFactory<T> : PlaceholderFactory<T>, IPopupFactory where T : PopupContainerBase
{
    [Inject]
    private readonly DiContainer _container;

    public Type PopupType => typeof(T);

    protected DiContainer Container => _container;

    public PopupContainerBase Create(GameObject prefab)
    {
        return _container.InstantiatePrefabForComponent<T>(prefab);
    }
}