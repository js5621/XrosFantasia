using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItemInfo : MonoBehaviour
{
    private Sprite cardSprite;
    private string cardName;
    private string aPoint;

    public CardItemInfo(Sprite cardSprite, string cardName ,string aPoint)
    {

        this.cardSprite = cardSprite;
        this.cardName = cardName;
        this.aPoint = aPoint;

    }
    public Sprite GetSprite()
    {
        return cardSprite;

    }
    public string GetCardName()
    {
        return cardName;
    }


    public string GetAPoint()
    {
        return aPoint;
    }

    public CardItemInfo GetCardItemInfo()
    {
        return this;
    }
}
