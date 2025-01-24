using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour {
    
    [SerializeField] private Transform _interactionObjectsParent;

    [field: SerializeField] public EnvironmentTypes Type { get; private set; }
    [field: SerializeField] public List<Transform> SpawnPoints;
    [field: SerializeField] public List<ObjectInteraction> ObjectInteractions { get; private set; }

    private EnvironmentEditorData _editorData;

    public void Init() {
        if (ObjectInteractions == null)
            return;

        ObjectInteractions.AddRange(_interactionObjectsParent.GetComponentsInChildren<ObjectInteraction>());
    }

    public EnvironmentEditorData GetEnvironmentEditorData() {
        _editorData = new EnvironmentEditorData();

        foreach (ObjectInteraction item in ObjectInteractions) {
            _editorData.AddObjectInteractionData(item.GetData());
        }

        return _editorData;
    }
}
