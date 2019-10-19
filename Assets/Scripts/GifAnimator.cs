using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public double framesPerSecond = 10.0;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int index = (int) (Time.time * framesPerSecond) % frames.Length;
        spriteRenderer.sprite = frames[index];

    }
}