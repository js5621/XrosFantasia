using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class PlayerSciptControl : MonoBehaviour
{
    // Start is called before the first frame update
    CardItemMakingScript cardItemMake;
    TurnManagerControlScipt turManage;
    [SerializeField] TextMeshPro ManaUITM;
    [SerializeField] AudioSource audioEffect;
    [SerializeField] AudioClip manaClip;
    public bool isPlayerControlAble= false;
    public bool playerManaSetMode = false;

    async void Start()
    {

        cardItemMake = FindAnyObjectByType<CardItemMakingScript>();
        turManage = FindAnyObjectByType<TurnManagerControlScipt>();

    }

    // Update is called once per frame
    async void Update()
    {
        if(cardItemMake.isPlayerTurnSet&&isPlayerControlAble)
            await ScoreSet();

    }

    async UniTask ScoreSet()
    {

        if (playerManaSetMode)
            return;

        int tmpMananum = 0;
        turManage.textMessageStateNum = 1;
        turManage.isTextProcess = false;
        playerManaSetMode = true;
        await UniTask.Delay(200);
        audioEffect.PlayOneShot(manaClip);
        await UniTask.Delay(1000);
        tmpMananum =int.Parse(ManaUITM.text)+2;
        ManaUITM.text = tmpMananum.ToString();

    }
}
