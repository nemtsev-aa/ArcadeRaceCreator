using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ProjectStageConfigs), menuName = "Configs/" + nameof(ProjectStageConfigs))]
public class ProjectStageConfigs : ScriptableObject {

    [field: SerializeField] public Sprite Lock { get; private set; }
    [field: SerializeField] public Sprite Complite { get; private set; }
    [field: SerializeField] public Sprite Fail { get; private set; }


    [field: SerializeField] public List<ProjectStageConfig> Configs { get; private set; }
}
