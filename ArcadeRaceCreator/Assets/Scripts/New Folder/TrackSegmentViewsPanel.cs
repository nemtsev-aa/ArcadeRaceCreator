using UnityEngine;

public class TrackSegmentViewsPanel : MonoBehaviour {
    [SerializeField] private TrackGridCellManager _elementSelector;
    [SerializeField] private TrackGeneratorConfigs _trackGeneratorConfigs;

    private TrackSegmentView[] _views;
    private TrackSegmentConfig _currentTrackSegmentConfig;

    void Start() {
        _views = GetComponentsInChildren<TrackSegmentView>();

        foreach (TrackSegmentView element in _views) {
            element.Init(_trackGeneratorConfigs.GetSegmentConfigByType(element.Type));
            element.Selected += OnElementSelected;
        }
    }

    private void OnElementSelected(TrackSegmentConfig config) {
        _currentTrackSegmentConfig = config;
        _elementSelector.SetContentToCell(_currentTrackSegmentConfig);
    }
}
