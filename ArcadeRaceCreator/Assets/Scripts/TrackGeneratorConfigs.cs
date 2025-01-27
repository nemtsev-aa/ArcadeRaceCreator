using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = nameof(TrackGeneratorConfigs), menuName = "Configs/" + nameof(TrackGeneratorConfigs))]
public class TrackGeneratorConfigs : ScriptableObject {
    [field: SerializeField] public List<TrackSegmentConfig> SegmentConfigs { get; private set; }

    public TrackSegmentConfig GetSegmentConfigByType(TrackSegmentType type) {
        return SegmentConfigs.FirstOrDefault(t => t.Type == type);
    }
}