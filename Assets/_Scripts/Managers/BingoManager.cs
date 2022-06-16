using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     Controls the logic during the gameplay from the Setup state until the End state.
/// </summary>
public class BingoManager : Singleton<BingoManager>
{
    /// <summary>
    ///     Time before calling the first number.
    /// </summary>
    [Tooltip("Time before calling the first number.")]
    public float nextSpawnTime = 5f;

    /// <summary>
    ///     Time between calling a new number.
    /// </summary>
    [Tooltip("Time between calling a new number.")]
    public float spawnInterval = 5f;

    [SerializeField]
    private Sprite cellBackground;

    [SerializeField]
    private Player player;

    [SerializeField]
    private Stopwatch stopwatch;

    [SerializeField]
    private GameObject ballPool;

    [SerializeField]
    private BallSpawner ballSpawner;

    [SerializeField]
    private EndScreen endScreen;

    /// <summary>
    ///     Whether the game is currently playing. Or in GameState.Play.
    /// </summary>
    private bool playing = false;

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

    private static readonly System.Random random = new System.Random();

    protected override void Awake() {
        base.Awake();

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        if (state == GameState.SetUp) {
            CreateCards();

            // Give players a few seconds to look into their card(s).
            StartCoroutine(AnalyzingPhase());
        };

        if (state == GameState.Play) {
            stopwatch.StartStopwatch();
            player.gameObject.SetActive(true);
            playing = true;
        };
        
        if (state == GameState.Pause) {
            stopwatch.StopStopwatch();
            player.gameObject.SetActive(false);
            playing = false;
        };
        
        if (state == GameState.Results) {
            stopwatch.StopStopwatch();
            playing = false;

            player.gameObject.SetActive(false);
            ballPool.gameObject.SetActive(false);

            endScreen.CollectResults(TotalBingoCards, TotalBingos, stopwatch.CurrentTime);
        }
    }

    /// <summary>
    ///     Initialize the list of called numbers.
    /// </summary>
    void Start() {
        AvailableNumbers = Enumerable.Range(1, 75).ToList();
        CalledNumbers = new List<int>();
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

    /// <summary>
    ///     Create the bingo card(s).
    /// </summary>
    public void CreateCards() {
        BingoCards = new BingoCard[TotalBingoCards];

        int horizontalCells = 5;
        int verticalCells = 5;
        Color textColor = Color.black;
        float cellSize = 7f;
        int fontSize = 40;

        // With 5x5 with size 7f, these result in -17.5
        float correctOriginPositionX = - (horizontalCells * cellSize / 2);
        float correctOriginPositionY = - (verticalCells * cellSize / 2);

        if (TotalBingoCards == 1) {
            BingoCards[0] = new BingoCard(horizontalCells, verticalCells, cellSize, textColor, fontSize, new Vector3(correctOriginPositionX, correctOriginPositionY), cellBackground);
        }
        else if (TotalBingoCards == 2) {
            BingoCards[0] = new BingoCard(horizontalCells, verticalCells, cellSize, textColor, fontSize, new Vector3(2.5f, correctOriginPositionY), cellBackground);
            BingoCards[1] = new BingoCard(horizontalCells, verticalCells, cellSize, textColor, fontSize, new Vector3(-37.5f, correctOriginPositionY), cellBackground);
        }
        else {
            BingoCards[0] = new BingoCard(horizontalCells, verticalCells, cellSize, textColor, fontSize, new Vector3(22.5f, correctOriginPositionY), cellBackground);
            BingoCards[1] = new BingoCard(horizontalCells, verticalCells, cellSize, textColor, fontSize, new Vector3(correctOriginPositionX, correctOriginPositionY), cellBackground);
            BingoCards[2] = new BingoCard(horizontalCells, verticalCells, cellSize, textColor, fontSize, new Vector3(-57.5f, correctOriginPositionY), cellBackground);
        }
    }

    /// <summary>
    ///     Give the player some time to look into their bingo card.
    /// </summary>
    /// <returns></returns>
    IEnumerator AnalyzingPhase() {
        yield return new WaitForSeconds(nextSpawnTime);

        GameManager.Instance.UpdateGameState(GameState.Play);
    }

    /// <summary>
    ///     Manage the games progress with the stopwatch.
    /// </summary>
    void Update() {
        if (playing) {
            if (stopwatch.CurrentTime > nextSpawnTime &&
                AvailableNumbers.Count > 0) {
                nextSpawnTime += spawnInterval;
                CallNewNumber();
            }

            if (bingosLeft <= 0) {
                GameManager.Instance.UpdateGameState(GameState.Results);
            }
        }
    }

    /// <summary>
    ///     Call a new number and remove it from the available numbers list. Then spawn a ball with that number.
    /// </summary>
    public void CallNewNumber() {
        int number = GetRandomItemAndRemoveIt();
        Color color = GetColor(number, out char colorCode);
        Debug.Log("Number: " + colorCode + "-" + number + ".");

        CalledNumbers.Add(number);

        ballSpawner.SpawnBall(number, color);
    }

    /// <summary>
    ///     Get a random item from a list and then remove it, to prevent duplicate numbers.
    /// </summary>
    /// <returns>The value to be displayed in the TextMesh.</returns>
    private int GetRandomItemAndRemoveIt() {
        int randomItem = AvailableNumbers[random.Next(AvailableNumbers.Count)];
        AvailableNumbers.Remove(randomItem);
        return randomItem;
    }

    /// <summary>
    ///     Get the color and color code (respective BINGO-letter) from the number.
    /// </summary>
    /// <param name="number">Given number.</param>
    /// <param name="colorCode">Respective color code to number.</param>
    /// <returns>Color belonging to number.</returns>
    private Color GetColor(int number, out char colorCode) {
        if (number <= 15) {
            colorCode = 'B';
            return Color.red;
        }
        else if (number <= 30) {
            colorCode = 'I';
            return Color.yellow;
        }
        else if (number <= 45) {
            colorCode = 'N';
            return Color.magenta;
        }
        else if (number <= 60) {
            colorCode = 'G';
            return Color.green;
        }
        else
            colorCode = 'O';
            return Color.blue;
    }


    // TODO: What if a double or triple bingo is found when there is only 1 bingo left?
    /// <summary>
    ///     Remove the found bingos from the bingos-left screen.
    /// </summary>
    /// <param name="bingosFound"></param>
    public void SubtractFoundBingos(int bingosFound) {
        bingosLeft -= bingosFound;
    }
}
