using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestDotweenMove : MonoBehaviour
{
    [SerializeField] Transform testPosition;// Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    [SerializeField]AudioSource audioSource;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.DOColor(Color.red, 1.2f);
        audioSource.DOFade(1f,3.3f);
        audioSource.DOPitch(1.7f, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
