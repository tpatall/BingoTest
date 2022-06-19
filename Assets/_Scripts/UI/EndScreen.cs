using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCards;
    [SerializeField] private TextMeshProUGUI textBingos;
    [SerializeField] private TextMeshProUGUI textTime;

    public void CollectResults(int cards, int bingos, float time) {
        textCards.text = cards.ToString();
        textBingos.text = bingos.ToString();
        textTime.text = time.ToString() + "s";
    }
}
