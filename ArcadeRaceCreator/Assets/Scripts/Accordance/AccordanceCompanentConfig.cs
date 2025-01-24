using UnityEngine;

[CreateAssetMenu(fileName = nameof(AccordanceCompanentConfig), menuName = "Configs/" + nameof(AccordanceCompanentConfig))]
public class AccordanceCompanentConfig : ScriptableObject {
    [field: SerializeField] public Color DefaultColor { get; private set; }
    [field: SerializeField] public Color SelectionColor { get; private set; } = Color.blue;
    [field: SerializeField] public Color TrueVerificationColor { get; private set; } = Color.green;
    [field: SerializeField] public Color FalseVerificationColor { get; private set; } = Color.red;
}
