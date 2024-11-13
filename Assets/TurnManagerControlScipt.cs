using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class TurnManagerControlScipt : MonoBehaviour
{
    [SerializeField] SpriteRenderer textSquare;
    [SerializeField] TextMeshPro turnStateText;
    PlayerSciptControl playerControl;
    VillainControlScript villainControl;
    bool processCheck =false;
    public bool isPlayerTurn = true;
    public int textMessageStateNum = 0;
    public bool textOn;
    public bool isTextProcess;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = FindAnyObjectByType<PlayerSciptControl>();
        villainControl = FindAnyObjectByType<VillainControlScript>();

    }

    // Update is called once per frame
    async void Update()
    {
        TurnCheck();
        if (textMessageStateNum != 0)
            await TextChange();




    }

    void TurnCheck()
    {
        if (processCheck)
            return;
        processCheck = true;
        if(isPlayerTurn&&!playerControl.isPlayerControlAble)
        {
            isPlayerTurn = false;
            villainControl.isVillainMove = true;
            villainControl.villainMoveFlag = false;
            villainControl.villainManaSetMode = false;
        }

        else if(!isPlayerTurn&&!villainControl.isVillainMove)
        {
            isPlayerTurn = true;
            playerControl.isPlayerControlAble = true;
            playerControl.playerManaSetMode =false;
        }


        processCheck = false;


    }
    async UniTask TextChange()
    {
        if (isTextProcess)
            return;
        isTextProcess = true;
        turnStateText.text = TurnText(textMessageStateNum);
        textSquare.DOFade(1, 0.7f);
        await UniTask.Delay(800);
        turnStateText.DOFade(1, 0.8f);
        await UniTask.Delay(1200);

        turnStateText.DOFade(0, 0.8f);
        await UniTask.Delay(500);
        textSquare.DOFade(0, 0.8f);
        await UniTask.Delay(500);
        textMessageStateNum = 0;


    }

    string TurnText(int stateNumber)
    {
        switch (stateNumber)
        {
            case 1:
                return "Player Turn";
            case 2:
                return "Enemy Turn";
            case 3:
                return "Enemy Attack";
            default:
                return "";


        }

    }
}
