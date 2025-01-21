using UnityEngine;
using System;

[Serializable]
public class BuildingMenuViewConfig : UICompanentConfig {
    [field: SerializeField] public BuildingFunctionTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    public override void OnValidate() {
        
    }
}
