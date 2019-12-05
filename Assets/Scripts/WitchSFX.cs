using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSFX : MonoBehaviour
{

    public AudioClip moveup;
    public AudioClip moveupgood;
    public AudioClip movedown;
    public AudioClip movedowngood;
    public AudioClip takedamage;
    public AudioClip bomb;
    AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageSFX()
    {
        AudioSource.PlayOneShot(takedamage, 0.8f);
    }

    public void MoveUpSFX()
    {
        AudioSource.PlayOneShot(moveup, 0.5f);
    }

    public void MoveUpGoodSFX()
    {
        AudioSource.PlayOneShot(moveupgood, 0.5f);
    }

    public void MoveDownSFX()
    {
        AudioSource.PlayOneShot(movedown, 0.5f);
    }

    public void MoveDownGoodSFX()
    {
        AudioSource.PlayOneShot(movedowngood, 0.5f);
    }

    public void BombSFX()
    {
        AudioSource.PlayOneShot(bomb, 0.8f);
    }
}
