using UnityEngine;

public class ProjectStageViewConfig : UICompanentConfig {
    public ProjectStageViewConfig(ProjectStageConfig stageConfig) {
        
        Type = stageConfig.Type;
        Name = stageConfig.Name;
        Description = stageConfig.Description;
        Icon = stageConfig.Icon;
        Lock = stageConfig.Lock;
        Complite = stageConfig.Complite;
        Fail = stageConfig.Fail;
    }

    public ProjectStageTypes Type { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Sprite Icon { get; private set; }
    public Sprite Lock { get; private set; }
    public Sprite Complite { get; private set; }
    public Sprite Fail { get; private set; }

    public override void OnValidate() {

    }
}


