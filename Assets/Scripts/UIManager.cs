using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject rulesPanel, gamePanel, pausePanel, endPanel;

    [SerializeField]
    private TextMeshProUGUI stateText;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        rulesPanel.SetActive(state == GameState.SetUp);
        gamePanel.SetActive(state == GameState.Play);
        //pausePanel.SetActive(state == GameState.Pause);
        //endPanel.SetActive(state == GameState.End);
    }
}
