using System;
using UnityEngine;

public interface IPopupFactory
{
    PopupContainerBase Create(GameObject prefab);
    Type PopupType { get; }
}