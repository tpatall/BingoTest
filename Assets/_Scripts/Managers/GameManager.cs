using System;
using UnityEngine;

/// <summary>
///     Controls the enum-based state switching.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnGameStateChanged;

    public GameState State { get; private set; }

    private void Start() {
        UpdateGameState(GameState.Rules);
    }

    public void UpdateGameState(GameState newstate) {
        State = newstate;

        switch (newstate) {
            // The 'Rules' state does not need extra functions as the UIManager is subscribed to this.
            case GameState.Rules:
                break;
            case GameState.SetUp:
                HandleSetup();
                break;
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.Pause:
                HandlePause();
                break;
            case GameState.Results:
                HandleResults();
                break;
            case GameState.End:
                HandleEnd();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newstate), newstate, null);
        }

        OnGameStateChanged?.Invoke(newstate);
    }

    private void HandleSetup() {
        Debug.Log("State change: Setup");
    }

    private void HandlePlay() {
        Debug.Log("State change: Play");
    }

    private void HandlePause() {
        Debug.Log("State change: Pause");
    }

    private void HandleResults() {
        Debug.Log("State change: Results");
    }

    private void HandleEnd() {
        Debug.Log("State change: End");
        Loader.Load(Scene.MainMenu);
    }
}

public enum GameState
{
    Rules,
    SetUp,
    Play,
    Pause,
    Results,
    End
}
