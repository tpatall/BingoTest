using UnityEngine;

/// <summary>
///     Controls UI-related functionality that affects or is affected by game-states.
/// </summary>
public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject rulesPanel, gamePanel, pausePanel, resultsPanel, background;

    protected override void Awake() {
        base.Awake();

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        rulesPanel.SetActive(state == GameState.Rules);
        // Keep the game panel active in the background when paused.
        gamePanel.SetActive(state == GameState.Play || state == GameState.Pause);
        pausePanel.SetActive(state == GameState.Pause);
        resultsPanel.SetActive(state == GameState.Results);
    }

    public void BeginPressed() {
        GameManager.Instance.UpdateGameState(GameState.SetUp);

        LoopMovement[] rows = background.GetComponentsInChildren<LoopMovement>();
        for (int i = 0; i < rows.Length; i++) {
            rows[i].StartAnimation();
        }
    }

    public void PausePressed() {
        GameManager.Instance.UpdateGameState(GameState.Pause);
    }

    public void UnpausePressed() {
        GameManager.Instance.UpdateGameState(GameState.Play);
    }

    public void RestartPressed() {
        Loader.Load(Scene.GameScene);
    }

    public void QuitPressed() {
        Loader.Load(Scene.MainMenu);
    }
}
