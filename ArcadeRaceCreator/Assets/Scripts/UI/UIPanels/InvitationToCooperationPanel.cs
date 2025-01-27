using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvitationToCooperationPanel : UIPanel {
    [SerializeField] private InvitationToCooperationPanelConfig _config;

    [SerializeField] private RectTransform _moreInfoFromProject;
    [SerializeField] private Image _qrGit;
    [SerializeField] private Image _qrBuild;
    [Space(10)]
    [SerializeField] private RectTransform _contacts;
    [SerializeField] private Image _qrKvantorium;
    [SerializeField] private Image _qrLector;

    [SerializeField] private Button _switchToContacts;

    public void Init() {
        UpdateSprites();
        AddListeners();
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value) {
            ShowInfoFromProject(true);
        }
    }

    public override void AddListeners() {
        base.AddListeners();

        _switchToContacts.onClick.AddListener(SwitchToContactsClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _switchToContacts.onClick.RemoveListener(SwitchToContactsClick);
    }

    private void UpdateSprites() {
        _qrGit.sprite = _config.QR_GIT;
        _qrBuild.sprite = _config.QR_Build;

        _qrKvantorium.sprite = _config.QR_Kvantorium;
        _qrLector.sprite = _config.QR_Lector;
    }

    private void SwitchToContactsClick() {
        ShowInfoFromProject(_moreInfoFromProject.gameObject.activeInHierarchy == true ? false : true);
    }

    private void ShowInfoFromProject(bool status) {
        _moreInfoFromProject.gameObject.SetActive(status);
        _contacts.gameObject.SetActive(!status);

        if (status == true)
            _switchToContacts.GetComponentInChildren<TextMeshProUGUI>().text = "Контакты";
        else
            _switchToContacts.GetComponentInChildren<TextMeshProUGUI>().text = "Информация";
    }
}
