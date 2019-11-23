using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public double framesPerSecond = 10.0;
    SpriteRenderer spriteRenderer;
    bool damage;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (damage == true)
        {
            spriteRenderer.enabled = true;
            int index = (int)(Time.time * framesPerSecond) % frames.Length;
            spriteRenderer.sprite = frames[index];
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    public void DamageTrigger()
    {
        StartCoroutine(DamageAnimation());
    }

    public IEnumerator DamageAnimation()
    {
        damage = true;
        yield return new WaitForSeconds(1);
        damage = false;
    }
}