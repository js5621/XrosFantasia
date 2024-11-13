
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;


public class CardItemMakingScript : MonoBehaviour
{


    // Start is called before the first frame update

    //EnemySprite
    [SerializeField] SpriteRenderer EnemyRenderer;
    //CardItem Sprite
    [SerializeField] Sprite referSprite1;
    [SerializeField] Sprite referSprite2;
    [SerializeField] Sprite referSprite3;
    [SerializeField] Sprite referSprite4;
    [SerializeField] Sprite referSprite5;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform HandStartPoint;
    [SerializeField] Transform HandEndPoint;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] AudioSource eventAudioSource;
    [SerializeField] public AudioSource cardAudioSource;
    [SerializeField] AudioClip enemyEntranceFx;
    [SerializeField] AudioClip drawFx;
    public List<Card> MyDeck;
    public List<Card> MyHands;
    public bool isHandsAdd =false;
    public bool isHandsRemove =false;
    public bool mulliganControl;
    public bool onMulligan;
    public bool isPlayerTurnSet = false;
    const int initialHandNumber = 5;
    bool initialControl =false;

    Vector2 difference = Vector2.zero;
    PlayerSciptControl playerControl;
    void MakeCardList()
    {

        for (int i = 0; i < 20; i++)
        {
            CardItemInfo cardItem = MakeCardItemInfo();

            var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Quaternion.identity);
            var card = cardObject.GetComponent<Card>();
            card.CardSetting(cardItem.GetCardName(),cardItem.GetAPoint(),cardItem.GetSprite());
            MyDeck.Add(card);
        }



    }
    public void HandsArrangeMent(Transform leftTr, Transform rightTr, int objCount)
    {
        float[] objLerps = new float[objCount];

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.2f, 0.5f }; break;
            case 3: objLerps = new float[] { 0.2f, 0.5f, 0.7f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            if (objCount >= 4)
            {

                float curve = Mathf.Sqrt(Mathf.Pow(0.5f, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = 0.5f >= 0 ? curve : -curve;
                targetPos.y += curve;
                Quaternion tempQt= Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
                MyHands[i].transform.DORotateQuaternion(tempQt, 0.5f);
            }
            MyHands[i].transform.DOMove(targetPos,0.5f);
            MyHands[i].GetComponent<Order>().SetOriginOrder(i);

        }




    }
    public void CardHandsOut(Card card)
    {
        int numberOfIndex =0;

        numberOfIndex=MyHands.IndexOf(card);

        MyHands.Remove(card);
        isHandsRemove = true;


    }
    public void HandsAdded()
    {
        if (isHandsAdd)
        {
            isHandsAdd = false;
            HandsArrangeMent(HandStartPoint, HandEndPoint, MyHands.Count);
        }
    }

    public void HandsRemoved()
    {
        if (isHandsRemove)
        {
            isHandsRemove = false;
            HandsArrangeMent(HandStartPoint, HandEndPoint, MyHands.Count);
        }
    }
    CardItemInfo MakeCardItemInfo()
    {

        Sprite randSprite = null;// 랜덤 생성 스프라이트
        string randName = null;
        string randPoint = null;

        int randCardInfoDecision = 0;
        randCardInfoDecision = Random.Range(0, 5);
        switch (randCardInfoDecision)
        {
            case 0:
                randSprite= referSprite1;
                randName = "PIXY";
                randPoint = 1000.ToString();
                break;
            case 1:
                randSprite = referSprite2;
                randName = "ANGEL";
                randPoint = 1500.ToString();
                break;
            case 2:
                randSprite = referSprite3;
                randName = "MIKO";
                randPoint = 2000.ToString();
                break;
            case 3:
                randSprite = referSprite4;
                randName = "KIM";
                randPoint = 3000.ToString();
                break;
            case 4:
                randSprite = referSprite5;
                randPoint = 5000.ToString();
                randName = "Alice Arisa";
                break;




        }
        CardItemInfo CardInfo = new CardItemInfo(randSprite,randName, randPoint);
        return CardInfo;

    }
    async UniTask CardInitialDraw()
    {

        if (initialControl)
            return;
        initialControl = true;
        MyHands.Add(MyDeck[0]);
        MyHands[MyHands.Count - 1].OnBoard = true;
        cardAudioSource.PlayOneShot(drawFx);
        MyDeck.RemoveAt(0);
        HandsArrangeMent(HandStartPoint, HandEndPoint, MyHands.Count);
        await UniTask.Delay(800);
        initialControl = false;




    }

    async UniTask MulliganDraw()
    {

       if (onMulligan)
            return;
        onMulligan = true;
        MyHands.Add(MyDeck[0]);
        MyHands[MyHands.Count - 1].OnBoard = true;
        MyDeck.RemoveAt(0);
        HandsArrangeMent(HandStartPoint, HandEndPoint, MyHands.Count);
        await UniTask.Delay(400);
        onMulligan = false;
    }

    async UniTask EnemyEntrance()
    {
        await UniTask.Delay(1000);
        EnemyRenderer.DOFade(1, 3f);
        await UniTask.Delay(100);
        eventAudioSource.PlayOneShot(enemyEntranceFx);
        await UniTask.Delay(800);
        eventAudioSource.DOFade(0, 1.5f);
        await UniTask.Delay(1200);
    }
    async void Start()
    {
        await EnemyEntrance();

        MyDeck = new List<Card>();
        MyHands = new List<Card>();
        playerControl = FindAnyObjectByType<PlayerSciptControl>();
        MakeCardList();
        if (MyDeck != null)
        {
            for (int i = 0; i < 5; i++)
                await CardInitialDraw();
            isPlayerTurnSet = true;
        }



    }
    // Update is called once per frame

   async void Update()
   {
        if (mulliganControl)
        {
            for(int i=0; i < 5; i++)
            {
               await MulliganDraw();
            }
            mulliganControl = false;
        }

        HandsAdded();
        HandsRemoved();

    }
}

