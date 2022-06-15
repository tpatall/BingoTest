using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BingoGame : MonoBehaviour
{
    public static BingoGame Instance;

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
    private GameManager gameManager;

    /// <summary>
    ///     Reference to all players. In this case one.
    /// </summary>
    [SerializeField]
    private Player player;

    /// <summary>
    ///     Reference to the stopwatch.
    /// </summary>
    [SerializeField]
    private Stopwatch stopwatch;

    /// <summary>
    ///     Reference to the gameobject holding the ball pool.
    /// </summary>
    [SerializeField]
    private GameObject ballPool;

    /// <summary>
    ///     Reference to the BallSpawner.
    /// </summary>
    [SerializeField]
    private BallSpawner ballSpawner;

    /// <summary>
    ///     Reference to the results screen.
    /// </summary>
    [SerializeField]
    private EndScreen endScreen;

    /// <summary>
    ///     If the game has begun yet.
    /// </summary>
    private bool hasGameBegun = false;

    /// <summary>
    ///     If the game is currently paused.
    /// </summary>
    private bool isGamePaused = false;

    /// <summary>
    ///     Total bingos left to be found.
    /// </summary>
    private int bingosLeft;

    /// <summary>
    ///     Amount of cards the player will use.
    /// </summary>
    public int BingoCards { get; private set; }

    /// <summary>
    ///     Amount of bingos left to be called.
    /// </summary>
    public int BingosTotal { get; private set; }

    /// <summary>
    ///     List of all numbers that have not been called yet.
    /// </summary>
    public List<int> AvailableNumbers { get; private set; }

    /// <summary>
    ///     List of all numbers that have been called.
    /// </summary>
    public List<int> CalledNumbers { get; private set; }

    private static readonly System.Random random = new System.Random();

    private void Awake() {
        Instance = this;
    }

    /// <summary>
    ///     Initialize the list of called numbers.
    /// </summary>
    void Start() {
        AvailableNumbers = Enumerable.Range(1, 75).ToList();
        CalledNumbers = new List<int>();
    }

    /// <summary>
    ///     Manage the games progress with the stopwatch.
    /// </summary>
    void Update() {
        if (hasGameBegun && !isGamePaused) {

            if (stopwatch.CurrentTime > nextSpawnTime &&
                AvailableNumbers.Count > 0) {
                nextSpawnTime += spawnInterval;
                CallNewNumber();
            }

            if (bingosLeft <= 0) {
                EndGame();
            }
        }
    }

    /// <summary>
    ///     SetUp rules from player input.
    /// </summary>
    /// <param name="inputCards">Card amount from player input.</param>
    /// <param name="inputBingos">Total bingos to be found, from player input.</param>
    public void SetUp(int inputCards, int inputBingos) {
        BingoCards = inputCards;
        BingosTotal = inputBingos;
        bingosLeft = BingosTotal;

        player.SetUp(this);

        GameManager.Instance.UpdateGameState(GameState.Play);
        
        StartCoroutine(AnalyzingPhase());
    }

    /// <summary>
    ///     Give the player some time to look into their bingo card.
    /// </summary>
    /// <returns></returns>
    IEnumerator AnalyzingPhase() {
        yield return new WaitForSeconds(nextSpawnTime);

        hasGameBegun = true;
        stopwatch.StartStopwatch();
    }

    /// <summary>
    ///     Pause or unpause the game based on gamestate.
    /// </summary>
    public void PauseGame() {
        if (gameManager.State == GameState.Pause) {
            gameManager.UpdateGameState(GameState.Play);
            stopwatch.StartStopwatch();
            isGamePaused = false;
            Debug.Log("Unpause!");
        } else {
            stopwatch.StopStopwatch();
            gameManager.UpdateGameState(GameState.Pause);
            isGamePaused = true;
            Debug.Log("Pause!");
        }
    }

    /// <summary>
    ///     End the game and stop the timer.
    /// </summary>
    public void EndGame() {
        stopwatch.StopStopwatch();
        Debug.Log("Game Over!");
        
        hasGameBegun = false;
        player.gameObject.SetActive(false);
        ballPool.gameObject.SetActive(false);

        GameManager.Instance.UpdateGameState(GameState.End);
        endScreen.CollectResults(BingoCards, BingosTotal, stopwatch.CurrentTime);
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
