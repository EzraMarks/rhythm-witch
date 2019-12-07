using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoredisplay : MonoBehaviour
{

    string scoretext;
    float score;
    Text scorevalue;

    // Start is called before the first frame update
    void Start()
    {
        scorevalue = GetComponent<Text>();
        score = GameObject.Find("Scorekeeper").GetComponent<Scorekeeper>().score;
        scorevalue.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
