using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using Cysharp.Threading.Tasks;

public class Card : MonoBehaviour
{

    [SerializeField] TextMeshPro cardName;
    [SerializeField] TextMeshPro aPoint;
    [SerializeField] SpriteRenderer cardImageSpace;
    [SerializeField] Sprite backSprite;
    [SerializeField] AudioSource cardAudio;
    [SerializeField] AudioClip cardPlaceEffect;

    SpriteRenderer cardSpace;
    Sprite CardFrontSpriteValue;
    Vector2 difference = Vector2.zero;
    Vector3 backUpPosition = Vector3.zero;
    Vector3 cardAreaBkPostion = Vector3.zero;
    CardItemMakingScript cardItemMake;
    PlayerSciptControl playercontrol;
    VillainControlScript vilControl;

    string cardNameValue;
    string aPointValue;
    bool isDrag=false;
    bool isFront = true;
    bool onDropArea= false;
    bool fxTurnOn =false;
    bool isCardAreaMounted = false;
    bool haveAttackExperience= false;
    public bool OnBoard=false;
    public bool isHandReturn = false;
    public bool isShootMode = false;

    float firstClickTime;
    float timeBetweenClicks;

    int clickCounter = 0;
    bool coroutineAllowed;
    int tempIndex = 0;
    public void  CardSetting(string cardNameValue, string aPointValue, Sprite CardFrontSpriteValue)
    {
        this.cardNameValue = cardNameValue;
        this.aPointValue = aPointValue;
        this.CardFrontSpriteValue = CardFrontSpriteValue;

    }
    public void CardState()
    {
        if(isFront == true)
        {
            cardName.text = cardNameValue;
            aPoint.text = aPointValue;
            cardImageSpace.sprite = CardFrontSpriteValue;

        }
        else
        {
            cardSpace.sprite = backSprite;
            cardName.text = "";
            aPoint.text = "";


        }


    }



    // Start is called before the first frame update
    void Start()
    {
        cardSpace = GetComponent<SpriteRenderer>();
        cardItemMake = FindAnyObjectByType<CardItemMakingScript>();
        playercontrol = FindAnyObjectByType<PlayerSciptControl>();
        vilControl  = FindAnyObjectByType<VillainControlScript>();

        firstClickTime = 0f;
        timeBetweenClicks = 0.2f;
        clickCounter = 0;
        coroutineAllowed = true;

    }

    // Update is called once per frame
    void Update()
    {

        CardState();
    }
    private async void OnMouseDown()
    {
        if (!playercontrol.isPlayerControlAble)
            return;
        backUpPosition = transform.position;
        isHandReturn = false;
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition)- transform.position;
        tempIndex = cardItemMake.MyHands.IndexOf(this);


        if(OnBoard ==false)
        {
            Debug.Log("Click!! :"+clickCounter);
            if(clickCounter ==2)
            {
                clickCounter = 0;
                firstClickTime = 0f;
                coroutineAllowed = true;
            }

            clickCounter++;

            if(clickCounter==1&& coroutineAllowed)
            {

                firstClickTime = Time.time;
                await DoublClickDetection();

            }


        }

        else
            transform.DORotateQuaternion(Quaternion.Euler(transform.rotation.x, transform.rotation.y, -transform.rotation.z), 0.5f);
    }

    async UniTask  DoublClickDetection()
    {
        coroutineAllowed=false;
        while(Time.time < firstClickTime+timeBetweenClicks)
        {
            Debug.Log("Detection Start!!");
            if(clickCounter==2)
            {
               await AttackMotion();
                clickCounter = 0;
                firstClickTime = 0f;
                coroutineAllowed = true;
                Debug.Log("Detection Complete");
                break;
            }


            await UniTask.Yield();



        }


    }

    private void OnMouseDrag()
    {
        if (!playercontrol.isPlayerControlAble)
            return;
        isDrag = true;
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        cardItemMake.CardHandsOut(this);

    }

    private void OnMouseUp()
    {
        if (!playercontrol.isPlayerControlAble)
            return;
        isDrag = false;
        if ( OnBoard)
        {
            cardItemMake.MyHands.Insert(tempIndex, this);
            transform.DOMove(backUpPosition, 0.3f);
            cardItemMake.isHandsAdd = true;
            isHandReturn = true;
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        OnBoard = false;

        Debug.Log(collision.gameObject.name+" in");
        if (!isDrag&&!isHandReturn)
        {
            if (collision.gameObject.name.Contains("CardZone"))
            {



                cardAreaBkPostion = collision.gameObject.transform.position;
                if(!isShootMode)
                    transform.DOMove(collision.transform.position, 0.2f);
                isCardAreaMounted = true;
                if (!fxTurnOn)
                {
                    cardAudio.PlayOneShot(cardPlaceEffect);
                    fxTurnOn = true;
                }

                //playercontrol.isPlayerControlAble = false;




            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " out");
        if (collision.gameObject.name.Contains("CardZone"))
        {
            if(!isCardAreaMounted)
                OnBoard =true;




        }
    }

    async UniTask AttackMotion()
    {
        isShootMode = true;
        transform.DOMove(vilControl.transform.position, 0.4f);
        await UniTask.Delay(100);
        transform.DOMove(cardAreaBkPostion, 0.4f);
        await UniTask.Delay(100);
        haveAttackExperience = true;
        isShootMode=false;

    }

}
