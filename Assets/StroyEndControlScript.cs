using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroyEndControlScript : MonoBehaviour
{
    public bool isScriptEnd;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name.Contains("StoryText"))
        {
            Debug.Log("Enddd");
            isScriptEnd = true;

        }
    }


            void Update()
    {

    }
}
