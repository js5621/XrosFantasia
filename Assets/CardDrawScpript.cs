using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardDrawScpript : MonoBehaviour
{
    [SerializeField] Transform DrawPosition;
    CardItemMakingScript cardItem;
    public void CardDraw()
    {

        cardItem = FindAnyObjectByType<CardItemMakingScript>();
        cardItem.MyHands.Add(cardItem.MyDeck[0]);
        cardItem.MyHands[cardItem.MyHands.Count - 1].OnBoard = true;
        cardItem.MyDeck.RemoveAt(0);
        cardItem.isHandsAdd = true;



    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
