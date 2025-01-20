public interface IGameMode {
    void Init(ApplicationManager applicationManager);
    void Reset();
    void AddListener();
    void RemoveLisener();
    void StartGamePlay();
    void FinishGameplay(Switchovers switchover);
    void ShowRewarded();
}

public enum Switchovers {
    MainMenu,
    CurrentLevel,
    NextLevel,
    AnotherGameMod
}

