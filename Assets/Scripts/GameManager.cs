using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public void UpdateGameState(GameState newstate) {
        State = newstate;

        switch (newstate) {
            case GameState.Selector:
                break;
            case GameState.Play:
                break;
            case GameState.End:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newstate), newstate, null);
        }
    }

    private void Awake() {
        Instance = this;
    }
}

public enum GameState
{
    Selector,
    Play,
    End
}
