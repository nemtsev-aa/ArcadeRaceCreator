using UnityEngine;
using System;

[Serializable]
public class BuildingMenuItemConfig {
    [field: SerializeField] public BuildingFunctionType Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
