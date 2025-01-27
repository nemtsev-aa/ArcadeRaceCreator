using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour {

    [SerializeField] private Transform _interactionObjectsParent;

    [field: SerializeField] public EnvironmentTypes Type { get; private set; }
    [field: SerializeField] public List<SpawnPoint> SpawnPoints;
    [field: SerializeField] public List<ObjectInteraction> ObjectInteractions { get; private set; }

    private EnvironmentEditorData _editorData;

    public void Init() {
        if (ObjectInteractions == null)
            return;

        ObjectInteractions.AddRange(_interactionObjectsParent.GetComponentsInChildren<ObjectInteraction>());

        ShowAllSpawnPoints(false);
        InitInteractionObjects();
    }

    public EnvironmentEditorData GetEnvironmentEditorData() {
        _editorData = new EnvironmentEditorData();

        foreach (ObjectInteraction item in ObjectInteractions) {
            _editorData.AddObjectInteractionData(item.GetData());
        }

        return _editorData;
    }

    private void ShowAllSpawnPoints(bool status) {

        if (SpawnPoints.Count == 0)
            return;

        foreach (SpawnPoint iPoint in SpawnPoints) {
            iPoint.Show(status);
        }
    }

    private void InitInteractionObjects() {
        if (ObjectInteractions.Count == 0)
            return;

        foreach (ObjectInteraction item in ObjectInteractions) {
            item.Init();
        }
    }

    public void ActvatePhysicForInteractionObjects(bool status) {
        if (ObjectInteractions.Count == 0)
            return;

        foreach (ObjectInteraction item in ObjectInteractions) {
            item.SetPhysic(status);
        }
    }
}
