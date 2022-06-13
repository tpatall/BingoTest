using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        UpdateGameState(GameState.Selector);
    }

    public void UpdateGameState(GameState newstate) {
        State = newstate;

        switch (newstate) {
            case GameState.Selector:
                HandleSelector();
                break;
            case GameState.Play:
                break;
            case GameState.End:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newstate), newstate, null);
        }

        OnGameStateChanged?.Invoke(newstate);
    }

    private void HandleSelector() {
    }
}

public enum GameState
{
    Selector,
    Play,
    End
}
