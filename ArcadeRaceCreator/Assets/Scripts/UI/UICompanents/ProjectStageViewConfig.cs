using UnityEngine;

public class ProjectStageViewConfig : UICompanentConfig {
    public ProjectStageViewConfig(ProjectStageConfig stageConfig) {
        
        Type = stageConfig.Type;
        Name = stageConfig.Name;
        Description = stageConfig.Description;
        Icon = stageConfig.Icon;
    }

    public void SetProjectStageStateIcons(Sprite s1, Sprite s2, Sprite s3) {
        Lock = s1;
        Complite = s2;
        Fail = s3;
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


