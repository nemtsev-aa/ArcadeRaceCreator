using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CarClassConfig : UICompanentConfig {
    [field: SerializeField] public CarTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public List<CarConfig> Configs { get; private set; }

    public override void OnValidate() {
        
    }
}
