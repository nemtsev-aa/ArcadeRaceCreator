using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public partial class TrackGeneratorWithUI : MonoBehaviour {
    public GameObject straightSegmentPrefab;
    public GameObject rightTurnSegmentPrefab;
    public GameObject leftTurnSegmentPrefab;
    public GameObject startSegmentPrefab;
    public GameObject finishSegmentPrefab;

    public Slider trackLengthSlider;
    public Button generateButton;
    public Button saveButton;

    public GameObject speedBoostPrefab;
    public GameObject speedSlowdownPrefab;

    private List<GameObject> availableSegments;
    private List<TrackSegmentData> generatedTrack = new List<TrackSegmentData>();
    private HashSet<Vector3Int> occupiedPositions = new HashSet<Vector3Int>();

    void Start() {
        availableSegments = new List<GameObject> {
            straightSegmentPrefab,
            rightTurnSegmentPrefab,
            leftTurnSegmentPrefab
        };

        //generateButton.onClick.AddListener(() => {
        //    int trackLength = (int)trackLengthSlider.value;
        //    GetComponent<TrackPreview>().GeneratePreview(trackLength);
        //});

        saveButton.onClick.AddListener(SaveTrackToJson);
    }

    public void GenerateTrack() {
        int trackLength = (int)trackLengthSlider.value;
        generatedTrack.Clear();
        
        ClearTrack();
        occupiedPositions.Clear();

        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = Quaternion.identity;

        // Добавляем стартовый сегмент
        Instantiate(startSegmentPrefab, currentPosition, Quaternion.identity);
        AddSegmentToTrack("Start", currentPosition, Quaternion.identity);

        occupiedPositions.Add(Vector3Int.RoundToInt(currentPosition));

        for (int i = 0; i < trackLength - 2; i++) {
            GameObject nextSegment = GetRandomSegment();
            Vector3 newPosition = CalculateNextPosition(currentPosition, currentRotation, nextSegment.name);

            if (!IsPositionOccupied(newPosition)) {
                Instantiate(nextSegment, newPosition, currentRotation);
                AddSegmentToTrack(nextSegment.name, newPosition, currentRotation);
                occupiedPositions.Add(Vector3Int.RoundToInt(newPosition));

                if (nextSegment == rightTurnSegmentPrefab) {
                    currentRotation *= Quaternion.Euler(0, 90, 0);
                }
                else if (nextSegment == leftTurnSegmentPrefab) {
                    currentRotation *= Quaternion.Euler(0, -90, 0);
                }

                currentPosition = newPosition;
            }
            else {
                i--; // Если позиция занята, повторяем итерацию
            }
        }

        // Добавляем финишный сегмент
        Vector3 finalPosition = CalculateNextPosition(currentPosition, currentRotation, "Finish");
        Instantiate(finishSegmentPrefab, finalPosition, currentRotation);
        AddSegmentToTrack("Finish", finalPosition, currentRotation);

        // Спавним ускорители и замедлители
        //SpawnSpeedBoosts();
        //SpawnSpeedSlowdowns();
    }

    void AddSegmentToTrack(string segmentType, Vector3 position, Quaternion rotation) {
        TrackSegmentData segment = new TrackSegmentData {
            SegmentType = segmentType,
            Position = position,
            Rotation = rotation
        };

        generatedTrack.Add(segment);
    }

    GameObject GetRandomSegment() {
        return availableSegments[Random.Range(0, availableSegments.Count)];
    }

    void ClearTrack() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void SaveTrackToJson() {
        string json = JsonUtility.ToJson(new TrackData { Segments = generatedTrack });
        string path = Path.Combine(Application.persistentDataPath, "track.json");
        
        File.WriteAllText(path, json);
        Debug.Log($"Track saved to {path}");
    }

    //void SpawnSpeedBoosts() {
    //    SpeedBoostSpawner boostSpawner = gameObject.AddComponent<SpeedBoostSpawner>();

    //    boostSpawner.speedBoostPrefab = speedBoostPrefab;
    //    boostSpawner.numberOfBoosts = 5;
    //}

    //void SpawnSpeedSlowdowns() {
    //    SpeedSlowdownSpawner slowdownSpawner = gameObject.AddComponent<SpeedSlowdownSpawner>();

    //    slowdownSpawner.speedSlowdownPrefab = speedSlowdownPrefab;
    //    slowdownSpawner.numberOfSlowdowns = 5;
    //}

    bool IsPositionOccupied(Vector3 position) {
        return occupiedPositions.Contains(Vector3Int.RoundToInt(position));
    }

    Vector3 CalculateNextPosition(Vector3 currentPosition, Quaternion currentRotation, string segmentType) {
        float gridSize = 10f; // Размер ячейки сетки
        Vector3 forwardDirection = currentRotation * Vector3.forward;
        Vector3 newPosition = currentPosition + forwardDirection * gridSize;

        // Проверка поворотов для правильного размещения
        if (segmentType == "RightTurnSegment") {
            newPosition += currentRotation * Vector3.right * gridSize;
        }
        else if (segmentType == "LeftTurnSegment") {
            newPosition -= currentRotation * Vector3.right * gridSize;
        }

        return newPosition;
    }
}
