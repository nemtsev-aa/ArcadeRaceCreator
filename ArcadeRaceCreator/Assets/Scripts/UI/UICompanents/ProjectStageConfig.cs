using UnityEngine;
using System;

[Serializable]
public class ProjectStageConfig {
    [field: SerializeField] public ProjectStageTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public ProjectStageStateConfig StateConfig { get; private set; }

    public Sprite Lock => StateConfig.Lock;
    public Sprite Complite => StateConfig.Complite;
    public Sprite Fail => StateConfig.Fail;

}


