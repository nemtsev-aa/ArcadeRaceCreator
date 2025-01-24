using System;
using UnityEngine;

[Serializable]
public class EquationConfig {
    public EquationConfig(string description, string name) {
        Description = description;
        Name = name;
    }
    public EquationConfig(string description, string name, bool result) {
        Description = description;
        Name = name;
        Result = result;
    }

    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Color BaseColor { get; set; }  
    public bool Result { get; set; }
}
