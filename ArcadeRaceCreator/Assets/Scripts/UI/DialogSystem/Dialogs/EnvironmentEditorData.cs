using System;
using System.Collections.Generic;

[Serializable]
public class EnvironmentEditorData {
    public List<ObjectInteractionData> ObjectInteractionDataList;

    public EnvironmentEditorData() {
        ObjectInteractionDataList = new List<ObjectInteractionData>();
    }

    public void AddObjectInteractionData(ObjectInteractionData data) {
        if (ObjectInteractionDataList.Contains(data) == true)
            return;

        ObjectInteractionDataList.Add(data);
    }
}
