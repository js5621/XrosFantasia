using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceChangeControl : MonoBehaviour
{
    bool isGoingToLoading =false;
    int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {
        sceneNumber = PlayerPrefs.GetInt("SceneNumber");
    }

    // Update is called once per frame
    async void Update()
    {
        await CallNextScene();
    }
    async UniTask CallNextScene()
    {
        if (isGoingToLoading)
            return;
        isGoingToLoading = true;
        await UniTask.Delay(10000);
        if(sceneNumber ==5)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(3);
    }
}
