using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    
    [SerializeField]
    private Sprite[] cardSprites;

    private List<int> cardIDList = new List<int>();
    private List<Card> cardList = new List<Card>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateCardID();
        ShuffleCardID();
        InitBoard();
    }

    void GenerateCardID() {
        // 0, 0, 1, 1, 2, 2, 3, 3, ... , 9, 9
        for (int i = 0; i < cardSprites.Length; i++) {
            cardIDList.Add(i);
            cardIDList.Add(i);
        }
    }

    void ShuffleCardID() {
        int cardcount = cardIDList.Count;
        for ( int i = 0; i < cardcount; i++) {
            int randomIndex = Random.Range(i, cardcount);
            int temp = cardIDList[randomIndex];
            cardIDList[randomIndex] = cardIDList[i];
            cardIDList[i] = temp;
        }
    }

    void InitBoard() {
        float spaceY = 1.8f;
        // row (-2, -1, 0, 1, 2)
        // 0 - 2 = -2 *spaceY = -3.6
        // 1 - 2 = -1 *spaceY = -1.8
        // 2 - 2 = 0 *spaceY = 0
        // 3 - 2 = 1 *spaceY = 1.8
        // 4 - 2 = 2 *spaceY = 3.6

        // (row - (int)(rowCount / 2)) * spaceY

        float spaceX = 1.3f;
        // col (-1.5, -0.5, 0.5, 1.5)
        // 0 - 2 = -2 + 0.5 = -1.5
        // 1 - 2 = -1 + 0.5 = -0.5
        // 2 - 2 = 0 + 0.5 = 0.5
        // 3 - 2 = 1 + 0.5 = 1.5

        // (col - (colCount / 2)) * spaceX + (spaceX / 2)


        int rowCount = 5;
        int colCount = 4;

        int cardIndex = 0;

        for (int row = 0; row < rowCount; row++) {
            for (int col = 0; col < colCount; col++) {
                float posX = (col - (colCount / 2)) * spaceX + (spaceX / 2);
                float posY = (row - (int)(rowCount / 2)) * spaceY;
                Vector3 pos = new Vector3(posX, posY, 0f);
                GameObject cardObject = Instantiate(cardPrefab, pos, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>();
                int cardID = cardIDList[cardIndex++];
                card.SetCardID(cardID);
                card.SetAnimalSprite(cardSprites[cardID]);
                cardList.Add(card);
            }
        }
    }

    public List<Card> GetCards() {
        return cardList;
    }
}
