using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class DialogueControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]SpriteRenderer CurtainRenderer;
    [SerializeField] AudioSource MainBgmSource;
    [SerializeField] AudioClip MainBgmClip1;
    [SerializeField] TextMeshPro dialogueText;
    [SerializeField] SpriteRenderer villainRenderer;
    [SerializeField] SpriteRenderer playerRenderer;
    public string[] lines;
    public float textSpeed;
    const int MaxIndex = 7;
    private int index;
    Color chDefaultColor;


    async void Start()
    {

        CurtainRenderer.DOFade(0, 5f);
        await BgmControl();
        dialogueText.text = string.Empty;
        chDefaultColor = playerRenderer.color;
        StartDialogue();

    }

    // Update is called once per frame
    async void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            if(index<MaxIndex)
            {
                if(index==MaxIndex-1)
                {

                    await SceneChange();

                }
                index++;
                dialogueText.text = "";
                await TypeLine();

            }
        }
    }
    void StartDialogue()
    {
        index = 0;
        TypeLine();
    }

    async UniTask TypeLine()
    {
        if (lines[index].Contains("레나 :"))
        {
            villainRenderer.DOColor(Color.white, 3f);
            playerRenderer.DOColor(chDefaultColor,1f);
        }
        else if (lines[index].Contains("유이 :"))
        {
            playerRenderer.DOColor(Color.white, 3f);
            villainRenderer.DOColor(chDefaultColor, 1f);
        }
        foreach(char c in lines[index].ToCharArray())
        {
            if (c == '<' || c == 'b' || c == 'r')
                continue;
            else if (c == '>')
                dialogueText.text += "<br>";
            else
                dialogueText.text += c;
            await UniTask.Delay(70);
        }

    }
    async UniTask BgmControl ()
    {
        await UniTask.Delay(1000);
        MainBgmSource.PlayOneShot(MainBgmClip1);
        MainBgmSource.DOFade(1, 2f);
    }

    async UniTask SceneChange()
    {
        CurtainRenderer.DOFade(1, 5f);
        MainBgmSource.DOFade(0, 5f);
        PlayerPrefs.SetInt("SceneNumber", 5);
        await UniTask.Delay(6000);
        SceneManager.LoadScene(1);
    }
}
