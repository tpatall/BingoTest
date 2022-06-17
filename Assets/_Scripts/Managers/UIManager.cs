using UnityEngine;

/// <summary>
///     Controls UI-related functionality that affects game-states.
/// </summary>
public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject rulesPanel, gamePanel, pausePanel, resultsPanel;

    protected override void Awake() {
        base.Awake();

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        rulesPanel.SetActive(state == GameState.Rules);
        gamePanel.SetActive(state == GameState.Play);
        pausePanel.SetActive(state == GameState.Pause);
        resultsPanel.SetActive(state == GameState.Results);
    }

    public void BeginPressed() {
        GameManager.Instance.UpdateGameState(GameState.SetUp);
    }

    public void PausePressed() {
        GameManager.Instance.UpdateGameState(GameState.Pause);
    }

    public void UnpausePressed() {
        GameManager.Instance.UpdateGameState(GameState.Play);
    }

    public void EndPressed() {
        GameManager.Instance.UpdateGameState(GameState.End);
    }
}
