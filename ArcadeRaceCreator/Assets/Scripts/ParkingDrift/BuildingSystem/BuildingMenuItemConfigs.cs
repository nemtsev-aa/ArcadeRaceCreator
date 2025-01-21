using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildingMenuItemConfigs), menuName = "Configs/" + nameof(BuildingMenuItemConfigs))]
public class BuildingMenuItemConfigs : ScriptableObject {
    [field: SerializeField] public List<BuildingMenuViewConfig> Configs;

    public BuildingMenuViewConfig GetConfigByBuildingFunctionType(BuildingFunctionTypes type) {
        return Configs.FirstOrDefault(t => t.Type == type);
    }
}
