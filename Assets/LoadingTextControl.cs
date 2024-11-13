using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class LoadingTextControl : MonoBehaviour
{
    [SerializeField] TMP_Text LoadingText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LoadingText.ForceMeshUpdate();
        var textInfo = LoadingText.textInfo;

        for(int i = 0; i<textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];
            if(!charInfo.isVisible) continue;

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j<4;++j)
            {

                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j]= orig+new Vector3(0, Mathf.Sin(Time.time*2f+orig.x*2f)*0.5f, 0);
            }
        }

        for (int i = 0; i<textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            LoadingText.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
