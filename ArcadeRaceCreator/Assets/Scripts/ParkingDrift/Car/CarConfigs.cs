using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CarConfigs), menuName = "Configs/" + nameof(CarConfigs))]
public class CarConfigs : ScriptableObject {
    [field: SerializeField] public List<CarClassConfig> Configs { get; private set; }

    public List<CarConfig> GetCarConfigsByType(CarTypes type) {
        return Configs.FirstOrDefault(c => c.Type == type).Configs;
    }
}
