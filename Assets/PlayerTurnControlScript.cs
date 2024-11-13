using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnControlScript : MonoBehaviour
{
    PlayerSciptControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
       playerControl =FindAnyObjectByType<PlayerSciptControl>();
       playerControl.isPlayerControlAble = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerTurnEnd()
    {
        playerControl.isPlayerControlAble = false;
    }
}
