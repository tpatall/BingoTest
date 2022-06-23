using UnityEngine;

/// <summary>
///     Controls UI-related functionality that affects or is affected by game-states.
/// </summary>
public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject rulesPanel, pausePanel, resultsPanel;

    protected override void Awake() {
        base.Awake();

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        rulesPanel.SetActive(state == GameState.Rules);
        pausePanel.SetActive(state == GameState.Pause);
        resultsPanel.SetActive(state == GameState.Results);
    }

    public void BeginPressed() {
        rulesPanel.LeanMoveX(-1920f, 1f).setEaseInBack().setOnComplete(
            delegate () {
                GameManager.Instance.UpdateGameState(GameState.SetUp);
            });
    }

    public void PausePressed() {
        GameManager.Instance.UpdateGameState(GameState.Pause);
    }

    public void UnpausePressed() {
        GameManager.Instance.UpdateGameState(GameState.Play);
    }

    public void ShowResults() {
        resultsPanel.LeanMoveX(960f, 1f).setEaseInBack();
    }

    public void RestartPressed() {
        Loader.Load(Scene.GameScene);
    }

    public void QuitPressed() {
        Loader.Load(Scene.MainMenu);
    }
}
