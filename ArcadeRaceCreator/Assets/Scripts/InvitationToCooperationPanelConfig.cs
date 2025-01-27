using UnityEngine;

[CreateAssetMenu(fileName = nameof(InvitationToCooperationPanelConfig), menuName = "Configs/" + nameof(InvitationToCooperationPanelConfig))]
public class InvitationToCooperationPanelConfig : ScriptableObject {
    [field: SerializeField] public Sprite QR_GIT { get; private set; }
    [field: SerializeField] public Sprite QR_Build { get; private set; }
    [field: SerializeField] public Sprite QR_Kvantorium { get; private set; }
    [field: SerializeField] public Sprite QR_Lector { get; private set; }
}

