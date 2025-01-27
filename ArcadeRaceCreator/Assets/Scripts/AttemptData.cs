using System;
using System.Collections.Generic;

[Serializable]
public class AttemptData {
    public EnvironmentTypes EnvironmentType { get; private set; }
    public EnvironmentEditorData EnvironmentEditorData { get; private set; }
    public CarTypes CarType { get; private set; }
    public CodingMiniGameResult MiniGameResult { get; private set; }
    public GameplayResult GameplayResult { get; private set; }

    public void SetEnvironmentType(EnvironmentTypes environmentType) {
        EnvironmentType = environmentType;
    }

    public void SetEnvironmentEditorData(EnvironmentEditorData data) {
        EnvironmentEditorData = data;
    }

    public void SetCarType(CarTypes carType) {
        CarType = carType;
    }

    public void SetCodingMiniGameResult(CodingMiniGameResult result) {
        MiniGameResult = result;
    }

    public void SetGameplayResult(GameplayResult result) {
        GameplayResult = result;
    }
    
}
