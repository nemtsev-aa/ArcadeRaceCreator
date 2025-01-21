using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(EnvironmentConfigs), menuName = "Configs/" + nameof(EnvironmentConfigs))]
public class EnvironmentConfigs : ScriptableObject {
    [field: SerializeField] public List<EnvironmentConfig> Configs;

    public EnvironmentConfig GetEnvironmentConfigByType(EnvironmentTypes type) {
        return Configs.FirstOrDefault(t => t.Type == type);
    }
}
