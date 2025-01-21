using UnityEngine;
using System;

[Serializable]
public class EnvironmentConfig : UICompanentConfig {
    [field: SerializeField] public EnvironmentTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public EnvironmentManager Prefab { get; private set; }

    public override void OnValidate() {
        
    }
}
