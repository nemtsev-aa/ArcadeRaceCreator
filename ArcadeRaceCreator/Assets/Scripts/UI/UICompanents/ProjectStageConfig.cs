using UnityEngine;
using System;

[Serializable]
public class ProjectStageConfig {
    [field: SerializeField] public ProjectStageTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}


