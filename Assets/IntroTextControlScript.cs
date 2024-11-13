using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class IntroTextControlScript : MonoBehaviour
{
    StroyEndControlScript endControl;
    [SerializeField] TextMeshPro introText;
    [SerializeField] TextMeshPro storyText;
    [SerializeField] AudioSource BgmSource;
    [SerializeField] AudioSource EffectSource;
    [SerializeField] AudioClip textFx;
    [SerializeField] Transform Endpos;
    [SerializeField] SpriteRenderer wallPaperRenderer;
    Vector2 scrollPos = Vector2.zero;
    float textDuration = 400f;
    string[] introScripts = {"크로스 판타지아\n - 서막 -","사라져버린\n그림패 술사들" };
    int scriptCount = 0;
    bool isScriptStart=false;
    bool scriptControl= false;
    bool onTextChangeMode=false;
    bool isGoingToLoading = false;
    // Start is called before the first frame update

   async void Start()
    {
        endControl = FindAnyObjectByType<StroyEndControlScript>();
        await IntroTextControl();
        isScriptStart = true;

    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        textDuration = 30f;
        Debug.Log(textDuration);
    }
    private void OnMouseUp()
    {
        textDuration = 300f;
        Debug.Log(textDuration);
    }
    async void Update()
    {   if(isScriptStart)
            StroyScriptControl();
        if (endControl.isScriptEnd)
        {
            await FadeOutAndLoading();
        }
    }



    async UniTask IntroTextControl()
    {
        if (scriptControl)
            return;
        scriptControl = true;
        for (int i = 0; i < introScripts.Length; i++)
        {
            introText.text=introScripts[i];
            await UniTask.Delay(1000);
            EffectSource.PlayOneShot(textFx);
            introText.DOFade(1, 1.2f);
            await UniTask.Delay(1200);
            introText.DOFade(0, 1.2f);
            await UniTask.Delay(1200);


        }

        scriptControl = false;

    }
    async UniTask StroyScriptControl()
    {
        storyText.transform.DOMoveY(Endpos.position.y, textDuration);
        await UniTask.Delay(40000);

    }

    async UniTask FadeOutAndLoading()
    {
        if (isGoingToLoading)
            return;
        isGoingToLoading = true;
        BgmSource.DOFade(0f, 10f);
        wallPaperRenderer.DOFade(0, 3f);
        PlayerPrefs.SetInt("SceneNumber", 0);
        await UniTask.Delay(5000);
        SceneManager.LoadScene(1);
    }

}
