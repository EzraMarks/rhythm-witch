using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public double framesPerSecond = 10.0;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        int index = (int)(Time.time * framesPerSecond) % frames.Length;
        GetComponent<UnityEngine.UI.Image>().sprite = frames[index];
    }
}
