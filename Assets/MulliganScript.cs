
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class MulliganScript : MonoBehaviour
{
    [SerializeField] Transform CardSpawnPoint;
    CardItemMakingScript cardItem;
   async public void DoMulligan()
   {
        cardItem = FindAnyObjectByType<CardItemMakingScript>();
        await   MulliganDeckReturn();
        cardItem.MyDeck = ShuffleCardList(cardItem.MyDeck);
        Debug.Log(cardItem.MyDeck);
        cardItem.mulliganControl = true;

    }

    async UniTask MulliganDeckReturn()
    {

        int handCount = cardItem.MyHands.Count;
        for (int i = 0; i <handCount; i++)
        {
            Quaternion tempQt = Quaternion.Euler(cardItem.MyHands[0].transform.rotation.x, cardItem.MyHands[0].transform.rotation.y, -cardItem.MyHands[0].transform.rotation.z);
            cardItem.MyHands[0].transform.DORotateQuaternion(tempQt, 0.5f);
            await UniTask.Delay(100);
            cardItem.MyHands[0].transform.DOMove(CardSpawnPoint.position, 0.5f);
            await UniTask.Delay(100);
            cardItem.MyDeck.Add(cardItem.MyHands[0]);
            cardItem.MyHands.RemoveAt(0);
            await UniTask.Delay(100);


        }
        await UniTask.Delay(300);
    }


    public List<Card> ShuffleCardList(List<Card> myDeck)
    {
        List<Card> ShuffleTempList = new List<Card>();
        List<Card> tempList = new List<Card>();
        List<Card> tempList2 = new List<Card>();
        for(int i=0; i<myDeck.Count/2;i++)
        {
            tempList.Add(myDeck[i]);

        }
        for (int i = myDeck.Count / 2; i <myDeck.Count; i++)
        {
            tempList2.Add(myDeck[i]);

        }
        for(int i=0;i<myDeck.Count/2; i++)
        {
            ShuffleTempList.Add(tempList[i]);
            ShuffleTempList.Add(tempList2[i]);

        }

        for (int i = 0; i < myDeck.Count / 2; i++)
        {
            tempList.RemoveAt(0);
            tempList2.RemoveAt(0);
        }

        for (int i = 0; i < myDeck.Count / 2; i++)
        {
            tempList.Add(ShuffleTempList[i]);

        }

        for (int i = myDeck.Count / 2; i < myDeck.Count; i++)
        {
            tempList2.Add(ShuffleTempList[i]);

        }
        for (int i = 0; i < myDeck.Count; i++)
        {
            ShuffleTempList.RemoveAt(0);

        }

        for (int i = 0; i < myDeck.Count / 2; i++)
        {
            ShuffleTempList.Add(tempList[i]);
            ShuffleTempList.Add(tempList2[i]);

        }

        for (int i = myDeck.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            Card temp = ShuffleTempList[i];
            ShuffleTempList[i] = ShuffleTempList[j];
            ShuffleTempList[j] = temp;
        }

        return ShuffleTempList;
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
