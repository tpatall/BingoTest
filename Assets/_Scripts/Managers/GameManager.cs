using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     Controls the enum-based state switching.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    #region variables
    /// <summary>
    ///     Time between calling a new number.
    /// </summary>
    [Tooltip("Time between calling a new number.")]
    public float spawnInterval = 5f;

    [SerializeField] private Sprite cellBackground;
    [SerializeField] private Player player;
    [SerializeField] private Stopwatch stopwatch;
    [SerializeField] private BallSpawner ballSpawner;
    [SerializeField] private EndScreen endScreen;

    /// <summary>
    ///     Time for calling the next number.
    /// </summary>
    private float nextSpawnTime = 0f;

    /// <summary>
    ///     Total bingos left to be found.
    /// </summary>
    private int bingosLeft;

    /// <summary>
    ///     Amount of cards the player will use.
    /// </summary>
    public int TotalBingoCards { get; private set; }

    /// <summary>
    ///     Amount of bingos left to be called.
    /// </summary>
    public int TotalBingos { get; private set; }

    /// <summary>
    ///     Array of current bingo cards.
    /// </summary>
    public BingoCard[] BingoCards { get; private set; }

    /// <summary>
    ///     List of all numbers that have not been called yet.
    /// </summary>
    public List<int> AvailableNumbers { get; private set; }

    /// <summary>
    ///     List of all numbers that have been called.
    /// </summary>
    public List<int> CalledNumbers { get; private set; }
    #endregion

    public static event Action<GameState> OnGameStateChanged;

    public GameState State { get; private set; }

    private void Start() {
        AvailableNumbers = Enumerable.Range(1, 75).ToList();
        CalledNumbers = new List<int>();

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
            case GameState.Rules:
                // The 'Rules' state does not need extra functions as the UIManager is subscribed to this,
                // and the state is only UI.
                break;
            case GameState.SetUp:
                HandleSetUp();
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
            default:
                throw new ArgumentOutOfRangeException(nameof(newstate), newstate, null);
        }

        OnGameStateChanged?.Invoke(newstate);
    }

    /// <summary>
    ///     SetUp rules from player input.
    /// </summary>
    /// <param name="inputCards">Card amount from player input.</param>
    /// <param name="inputBingos">Total bingos to be found, from player input.</param>
    public void SetUp(int inputCards, int inputBingos) {
        TotalBingoCards = inputCards;
        TotalBingos = inputBingos;
        bingosLeft = TotalBingos;
    }

    private void HandleSetUp() {
        BingoCards = new BingoCard[TotalBingoCards];
        CreateCards();

        StartCoroutine(WaitJustOneMoment());

        IEnumerator WaitJustOneMoment() {
            yield return new WaitForFixedUpdate();

            UpdateGameState(GameState.Play);
        }
    }

    /// <summary>
    ///     Create the bingo card(s).
    /// </summary>
    private void CreateCards() {
        int horizontalCells = 5;
        int verticalCells = 5;
        Color textColor = Color.black;
        float cellSize = 7f;
        int fontSize = 40;

        // With 5x5 with size 7f, these result in -17.5
        float correctOriginPositionX = -(horizontalCells * cellSize / 2);
        float correctOriginPositionY = -(verticalCells * cellSize / 2) - 2.5f;

        for (int i = 0; i < TotalBingoCards; i++) {
            Vector2 origin;
            if (TotalBingoCards == 1) {
                origin = new Vector2(correctOriginPositionX, correctOriginPositionY);
            }
            else if (TotalBingoCards == 2) {
                origin = new Vector2(correctOriginPositionX - 20f, correctOriginPositionY);
            }
            else {
                origin = new Vector2(correctOriginPositionX - 40f, correctOriginPositionY);
            }

            BingoCards[i] = new BingoCard(horizontalCells, verticalCells, cellSize, new Vector3(origin.x + 40f * i, origin.y));
            BingoCards[i].Setup(gameObject, textColor, fontSize, cellBackground);
        }
    }

    private void HandlePlay() {
        stopwatch.StartStopwatch();

        player.gameObject.SetActive(true);
    }

    /// <summary>
    ///     Manage the games progress with the stopwatch.
    /// </summary>
    private void Update() {
        if (State == GameState.Play) {
            if (stopwatch.CurrentTime > nextSpawnTime &&
                AvailableNumbers.Count > 0) {
                nextSpawnTime += spawnInterval;
                CallNewNumber();
            }

            if (bingosLeft <= 0) {
                UpdateGameState(GameState.Results);
            }
        }
    }

    /// <summary>
    ///     Call a new number and remove it from the available numbers list. Then spawn a ball with that number.
    /// </summary>
    private void CallNewNumber() {
        int number = BingoUtils.GetRandomItemAndRemoveIt(AvailableNumbers);
        Color color = BingoUtils.GetBingoColor(number, out string letter);
        Debug.Log("Number: " + letter + "-" + number);

        CalledNumbers.Add(number);

        ballSpawner.SpawnBall(number, color);
    }

    /// <summary>
    ///     Remove the found bingos from the bingosLeft.
    /// </summary>
    /// <param name="bingosFound"></param>
    public void SubtractFoundBingos(int bingosFound) {
        bingosLeft -= bingosFound;
    }

    private void HandlePause() {
        stopwatch.StopStopwatch();

        player.gameObject.SetActive(false);
    }

    private void HandleResults() {
        stopwatch.StopStopwatch();

        player.gameObject.SetActive(false);
        ballSpawner.gameObject.SetActive(false);

        // TODO: Disable BingoCard gameObjects for a clean Results screen

        endScreen.CollectResults(TotalBingoCards, TotalBingos, stopwatch.CurrentTime);
    }
}

public enum GameState
{
    Rules,
    SetUp,
    Play,
    Pause,
    Results
}
