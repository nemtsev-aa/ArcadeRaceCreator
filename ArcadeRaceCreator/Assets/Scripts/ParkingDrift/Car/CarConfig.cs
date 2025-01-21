using UnityEngine;
using System;

[Serializable]
public class CarConfig : UICompanentConfig {
    
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Car Prefab { get; private set; }

    public override void OnValidate() {
        
    }
}
