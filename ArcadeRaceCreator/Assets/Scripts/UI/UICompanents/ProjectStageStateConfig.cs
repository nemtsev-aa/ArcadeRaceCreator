using UnityEngine;

[CreateAssetMenu(fileName = nameof(ProjectStageStateConfig), menuName = "Configs/" + nameof(ProjectStageStateConfig))]
public class ProjectStageStateConfig : ScriptableObject {

    [field: SerializeField] public Sprite Lock { get; private set; }
    [field: SerializeField] public Sprite Complite { get; private set; }
    [field: SerializeField] public Sprite Fail { get; private set; }

}
