using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class VillainControlScript : MonoBehaviour
{
    [SerializeField] public bool isVillainMove = false;
    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] GameObject ManaUI;
    [SerializeField] SpriteRenderer ManaUIRender;
    [SerializeField] GameObject ManaTMpro;
    [SerializeField] Transform fireBallPoint;
    [SerializeField] Transform targetHit;
    [SerializeField] TextMeshPro villainManaUITM;
    [SerializeField] AudioSource audioEffect;
    [SerializeField] AudioClip manaClip;
    [SerializeField] AudioClip fireballClip;
    public bool isVillainMoveEnd = false;
    public bool villainMoveFlag = false;
    public bool villainManaSetMode = false;
    bool villainHitflag = false;
    bool scoresetfinsh = false;
    TurnManagerControlScipt turManage;
    // Start is called before the first frame update
    void Start()
    {
        ManaUIRender.DOFade(1,2.5f);
        turManage = FindAnyObjectByType<TurnManagerControlScipt>();
    }

    // Update is called once per frame
    async void Update()
    {
        if (isVillainMove)
        {
            await ScoreSet();
            if (scoresetfinsh == true)
                await VillainMoveTest();

            Debug.Log(isVillainMove);


        }

    }

    async UniTask VillainMoveTest()
    {
        await UniTask.Delay(2000);
        if (villainMoveFlag)
            return;
        villainMoveFlag = true;
        turManage.textMessageStateNum = 3;
        turManage.isTextProcess = false;
        await UniTask.Delay(2000);
        audioEffect.PlayOneShot(fireballClip);
        await UniTask.Delay(100);
        var cardObject = Instantiate(fireBallPrefab, fireBallPoint.position, Quaternion.identity);
        await UniTask.Delay(500);
        cardObject.transform.DOScale(1.2f, 1f);
        await UniTask.Delay(1000);
        cardObject.transform.DOMove(targetHit.position, 0.5f);
        await UniTask.Delay(1000);
        Destroy(cardObject);
        await UniTask.Delay(2000);
        isVillainMove = false;

        scoresetfinsh = false;




    }



    async UniTask VillainHitAcceleration(GameObject villObject)
    {
        float accel = 2f;
        float tmp_accel = 0f;

        for (int i=0;i<10;i++)
        {
            tmp_accel = Mathf.Pow(accel, 2);
            await UniTask.Delay(200);
        }
    }

    async UniTask ScoreSet()
    {
        int tmpMananum = 0;
        if (villainManaSetMode)
            return;
        villainManaSetMode = true;
        turManage.textMessageStateNum = 2;
        turManage.isTextProcess = false;
        await UniTask.Delay(100);
        audioEffect.PlayOneShot(manaClip);
        await UniTask.Delay(1000);
        tmpMananum = int.Parse(villainManaUITM.text) + 2;
        villainManaUITM.text = tmpMananum.ToString();
        scoresetfinsh = true;
    }
}
