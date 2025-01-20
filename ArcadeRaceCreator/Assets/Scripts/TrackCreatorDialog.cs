using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrackCreatorDialog : MonoBehaviour, IDisposable {
    public event Action SaveTrackButtonClicked;
    public event Action CreateTrackButtonClicked;

    [SerializeField] private Button _saveTrack;
    [SerializeField] private Button _crateTrack;

    private void Start() {
        _saveTrack.onClick.AddListener(SaveTrackButton_Click);
        _crateTrack.onClick.AddListener(CreateTrackButton_Click);
    }

    private void SaveTrackButton_Click() {
        SaveTrackButtonClicked?.Invoke();
    }

    private void CreateTrackButton_Click() {
        CreateTrackButtonClicked?.Invoke();
    }

    public void Dispose() {
        _saveTrack.onClick.RemoveListener(SaveTrackButton_Click);
        _crateTrack.onClick.RemoveListener(CreateTrackButton_Click);
    }

    
}
