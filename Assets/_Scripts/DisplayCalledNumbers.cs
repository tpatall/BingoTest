using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCalledNumbers : MonoBehaviour
{
    [SerializeField]
    private BingoManager bingoGame;

    [SerializeField]
    private TextMeshProUGUI text;

    /// <summary>
    ///     Convert the called numbers to a text and display it.
    /// </summary>
    public void GetCalledNumbers() {
        List<int> list = bingoGame.CalledNumbers;
        if (list.Count > 0) {
            string numberString = "";
            for (int i = 0; i < list.Count; i++) {
                if (i < list.Count - 1) {
                    numberString += list[i] + " | ";
                } else {
                    numberString += list[i];
                }
            }

            text.text = numberString;
        } else {
            text.text = "No numbers were called yet.";
        }
    }
}
