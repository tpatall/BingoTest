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

    /// <summary>
    ///     Updates the game state to newstate.
    ///     Does not use any logic currently, so merge with BingoManager?
    /// </summary>
    /// <param name="newstate">Next state.</param>
    public void UpdateGameState(GameState newstate) {
        State = newstate;
        Debug.Log("State change: " + newstate);

        switch (newstate) {
            // The 'Rules' state does not need extra functions as the UIManager is subscribed to this.
            case GameState.Rules:
                //HandleRules();
                break;
            case GameState.SetUp:
                //HandleSetup();
                break;
            case GameState.Play:
                //HandlePlay();
                break;
            case GameState.Pause:
                //HandlePause();
                break;
            case GameState.Results:
                //HandleResults();
                break;
            case GameState.End:
                HandleEnd();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newstate), newstate, null);
        }

        OnGameStateChanged?.Invoke(newstate);
    }

    //private void HandleRules() { }

    //private void HandleSetup() { }

    //private void HandlePlay() { }

    //private void HandlePause() { }

    //private void HandleResults() { }

    private void HandleEnd() {
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
