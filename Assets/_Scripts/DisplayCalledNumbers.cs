using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class DisplayCalledNumbers : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    /// <summary>
    ///     Convert the called numbers list to a text and display it.
    /// </summary>
    public void GetCalledNumbers() {
        List<int> list = BingoManager.Instance.CalledNumbers;

        if (list.Count > 0) {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++) {
                if (i < list.Count - 1) {
                    builder.Append(list[i] + " | ");
                }
                else {
                    builder.Append(list[i]);
                }

                text.text = builder.ToString();
            }
        } else {
            text.text = "No numbers were called yet.";
        }
    }
}
