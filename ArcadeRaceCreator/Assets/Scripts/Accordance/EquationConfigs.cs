using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =nameof(EquationConfigs), menuName = "Configs/" + nameof(EquationConfigs))]
public class EquationConfigs : ScriptableObject {
    [field: SerializeField] public List<EquationConfig> Configs { get; private set; }
}
