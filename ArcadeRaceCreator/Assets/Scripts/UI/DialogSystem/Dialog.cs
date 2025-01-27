using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour, IDisposable {
    public static event Action BackClicked;
    public static event Action MainMenuClicked;

    [SerializeField] protected Button BackButton;
    [SerializeField] protected Button MainMenuButton;
    [SerializeField] protected Button ApplyButton;

    [SerializeField] protected List<UIPanel> Panels = new List<UIPanel>();

    public bool IsInit { get; protected set; } = false;
    public IReadOnlyList<UIPanel> DialogPanels => Panels;

    public virtual void Init() {
        if (IsInit == true)
            return;

        InitializationPanels();
        AddListeners();

        IsInit = true;
    }

    public virtual void Show(bool value) {
        gameObject.SetActive(value);

        if (value == true && ApplyButton != null) {
            ApplyButton.gameObject.SetActive(false);
        }
    }

    public virtual void ShowPanel<T>(bool value) where T : UIPanel {
        T panel = (T)Panels.First(panel => panel is T);

        panel.UpdateContent();
        panel.Show(value);
    }

    public virtual void ResetPanels() {
        if (Panels.Count == 0)
            return;

        foreach (var iPanel in Panels) {
            iPanel.Reset();
        }
    }

    public virtual void AddListeners() {
        if (BackButton != null)
            BackButton.onClick.AddListener(BackButtonClick);

        if (MainMenuButton != null)
            MainMenuButton.onClick.AddListener(MainMenuClick);
    }

    public virtual void RemoveListeners() {
        BackButton.onClick.RemoveListener(BackButtonClick);
        MainMenuButton.onClick.RemoveListener(MainMenuClick);
    }

    public virtual T GetPanelByType<T>() where T : UIPanel {
        return (T)Panels.FirstOrDefault(panel => panel is T);
    }

    public virtual void InitializationPanels() {

    }

    public virtual void PreparingForClosure() {

    }

    public virtual void BackButtonClick() {
        ResetPanels();

        BackClicked?.Invoke();
    }

    private void MainMenuClick() => MainMenuClicked?.Invoke();


    public void Dispose() {
        RemoveListeners();
    }
}