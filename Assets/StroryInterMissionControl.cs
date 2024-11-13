using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StroryInterMissionControl : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await    CallNextScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    async UniTask CallNextScene()
    {

        await UniTask.Delay(10000);
        SceneManager.LoadScene(5);
    }
}
