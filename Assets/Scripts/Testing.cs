using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private BingoCard card;
    private IList<int> items;

    // Start is called before the first frame update
    void Start()
    {
        card = new BingoCard(5, 5, 5f, Color.white, 10, new Vector3(-12.5f, -12.5f));

        //card1 = new BingoCard(5, 5, 5f, Color.white, 10, new Vector3(7.5f, -12.5f));
        //card2 = new BingoCard(5, 5, 5f, Color.yellow, 10, new Vector3(-32.5f, -12.5f));

        items = Enumerable.Range(1, 76).ToList();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the position that got clicked has a bingoCell.
            // If it does, check if the cell can get marked and if it does,
            // check the card if a new bingo is found.
            BingoCell bingoCell = card.GetCell(GetMouseWorldPosition());
            if (bingoCell.MarkCell(items)) {
                bingoCell.BingoCard.CheckForBingo();
            }

        }
    }

    private Vector3 GetMouseWorldPosition()
    {

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        //Debug.Log(pos);
        return pos;
    }


}
