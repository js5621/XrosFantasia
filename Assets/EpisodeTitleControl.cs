using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class EpisodeTitleControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer wallPaperRenderer;
    [SerializeField] TextMeshPro chapterTitle;
    [SerializeField]TextMeshPro episodeTitleText;
    [SerializeField] SpriteRenderer squareRend;
    async void Start()
    {

        wallPaperRenderer.DOFade(1, 10f);
        chapterTitle.DOFade(1, 10f);
        episodeTitleText.DOFade(1, 11.1f);
        await CurtainCall();
        await CallNextScene();
    }

    // Update is called once per frame
    void Update()
    {

    }
    async UniTask CurtainCall()
    {

        await UniTask.Delay(10000);
        squareRend.DOFade(1, 2f);
    }

    async UniTask CallNextScene()
    {

        await UniTask.Delay(2000);
        SceneManager.LoadScene(4);
    }
}
