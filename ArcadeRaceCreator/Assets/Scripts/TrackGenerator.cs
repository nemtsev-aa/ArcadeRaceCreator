using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TrackGenerator : MonoBehaviour {
    [SerializeField] private TrackCreatorDialog _trackCreatorDialog;
    [SerializeField] private TrackGeneratorConfigs _trackGeneratorConfigs;
    [SerializeField] private TrackGridCellManager _gridCellManager;

    private IReadOnlyList<TrackGridCell> Cells => _gridCellManager.TrackGridCells;

    private void Start() {
        _trackCreatorDialog.CreateTrackButtonClicked += OnCreateTrackButtonClicked;
        _trackCreatorDialog.SaveTrackButtonClicked += OnSaveTrackButtonClicked;
    }

    private void OnSaveTrackButtonClicked() {
        SaveTrackToJson();
    }

    private void OnCreateTrackButtonClicked() {
        
    }

    public void SaveTrackToJson() {
        List<TrackSegmentData> segmentDatas = new List<TrackSegmentData>();

        foreach (TrackGridCell iCell in Cells) {
            TrackSegmentData data = iCell.GetTrackSegmentData();

            if (data != null)
                segmentDatas.Add(data);
        }

        string json = JsonUtility.ToJson(new TrackData { Segments = segmentDatas });
        string path = Path.Combine(Application.persistentDataPath, "track.json");

        File.WriteAllText(path, json);
        Debug.Log($"Track saved to {path}");
    }

    //[SerializeField] private GameObject _trackGeneratorUI; // Ссылка на объект с TrackGeneratorWithUI

    //[SerializeField] private GridLayoutGroup _gridLayout; // GridLayoutGroup для отображения миниатюры
    //[SerializeField] private Slider _trackLengthSlider;
    //[SerializeField] private TextMeshProUGUI _sliderValue;

    //[SerializeField] private Button _confirmButton; // Кнопка для подтверждения трассы
    //[SerializeField] private TrackGeneratorConfigs _trackGeneratorConfigs;

    //private Sprite _straightSegmentSprite => _trackGeneratorConfigs.GetSegmentConfigByType(TrackSegmentType.Straight).Icon;
    //private Sprite _rightTurnSegmentSprite => _trackGeneratorConfigs.GetSegmentConfigByType(TrackSegmentType.RightTurn).Icon;
    //private Sprite _leftTurnSegmentSprite => _trackGeneratorConfigs.GetSegmentConfigByType(TrackSegmentType.LeftTurn).Icon;
    //private Sprite _startSegmentSprite => _trackGeneratorConfigs.GetSegmentConfigByType(TrackSegmentType.Start).Icon;
    //private Sprite _finishSegmentSprite => _trackGeneratorConfigs.GetSegmentConfigByType(TrackSegmentType.Finish).Icon;

    //private Dictionary<TrackSegmentType, Sprite> _segmentSprites = new Dictionary<TrackSegmentType, Sprite>();
    //private List<GameObject> _previewSegments = new List<GameObject>();

    //private void Start() {
    //    // Инициализируем словарь спрайтов для каждого типа сегмента
    //    _segmentSprites.Add(TrackSegmentType.Start, _startSegmentSprite);
    //    _segmentSprites.Add(TrackSegmentType.Finish, _finishSegmentSprite);
    //    _segmentSprites.Add(TrackSegmentType.Straight, _straightSegmentSprite);
    //    _segmentSprites.Add(TrackSegmentType.RightTurn, _rightTurnSegmentSprite);
    //    _segmentSprites.Add(TrackSegmentType.LeftTurn, _leftTurnSegmentSprite);

    //    //_confirmButton.onClick.AddListener(ConfirmTrack);
    //    _confirmButton.onClick.AddListener(() => GeneratePreview(Mathf.RoundToInt(_trackLengthSlider.value)));
    //    _trackLengthSlider.onValueChanged.AddListener((value) => _sliderValue.text = $"{Mathf.RoundToInt(value)}");

    //    _sliderValue.text = $"{Mathf.RoundToInt(_trackLengthSlider.value)}";
    //}

    //public void GeneratePreview(int trackLength) {
    //    ClearPreview();

    //    Vector2Int currentPosition = Vector2Int.zero;
    //    Quaternion currentRotation = Quaternion.identity;

    //    // Добавляем стартовый сегмент
    //    AddPreviewSegment(currentPosition, TrackSegmentType.Start);
    //    currentPosition += GetNextPosition(currentRotation, TrackSegmentType.Start);

    //    for (int i = 0; i < trackLength - 2; i++) {
    //        TrackSegmentType nextSegmentType = GetRandomSegment();
    //        AddPreviewSegment(currentPosition, nextSegmentType);

    //        currentPosition += GetNextPosition(currentRotation, nextSegmentType);

    //        if (nextSegmentType == TrackSegmentType.RightTurn) {
    //            currentRotation *= Quaternion.Euler(0, 90, 0);
    //        }
    //        else if (nextSegmentType == TrackSegmentType.LeftTurn) {
    //            currentRotation *= Quaternion.Euler(0, -90, 0);
    //        }
    //    }

    //    // Добавляем финишный сегмент
    //    AddPreviewSegment(currentPosition, TrackSegmentType.Finish);
    //}

    //private void AddPreviewSegment(Vector2Int position, TrackSegmentType segmentType) {
    //    GameObject segment = new GameObject($"{segmentType}");

    //    Image image = segment.AddComponent<Image>();
    //    image.sprite = _segmentSprites[segmentType];
    //    image.SetNativeSize();

    //    RectTransform rectTransform = segment.GetComponent<RectTransform>();
    //    rectTransform.SetParent(_gridLayout.transform);
    //    rectTransform.localPosition = new Vector3(position.x * _gridLayout.cellSize.x, position.y * _gridLayout.cellSize.y, 0);

    //    _previewSegments.Add(segment);
    //}

    //private void ClearPreview() {
    //    foreach (GameObject segment in _previewSegments) {
    //        Destroy(segment);
    //    }

    //    _previewSegments.Clear();
    //}

    //private TrackSegmentType GetRandomSegment() {
    //    return new[] { 
    //        TrackSegmentType.Straight, 
    //        TrackSegmentType.RightTurn,
    //        TrackSegmentType.LeftTurn 
    //    }

    //    [Random.Range(0, 3)];
    //}

    //private Vector2Int GetNextPosition(Quaternion rotation, TrackSegmentType segmentType) {
    //    Vector2Int direction = Vector2Int.up;

    //    if (rotation.eulerAngles.y == 90) {
    //        direction = Vector2Int.right;
    //    }
    //    else if (rotation.eulerAngles.y == 180) {
    //        direction = Vector2Int.down;
    //    }
    //    else if (rotation.eulerAngles.y == 270) {
    //        direction = Vector2Int.left;
    //    }

    //    if (segmentType == TrackSegmentType.RightTurn) {
    //        direction = new Vector2Int(-direction.y, direction.x); // Поворот направо
    //    }
    //    else if (segmentType == TrackSegmentType.LeftTurn) {
    //        direction = new Vector2Int(direction.y, -direction.x); // Поворот налево
    //    }

    //    return direction;
    //}

    //private void ConfirmTrack() {
    //    TrackGeneratorWithUI trackGenerator = _trackGeneratorUI.GetComponent<TrackGeneratorWithUI>();
    //    trackGenerator.GenerateTrack();
    //}
}