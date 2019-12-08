using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorekeeper : MonoBehaviour
{

    //Player score variable
    public float score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecordScore()
    {
        score = GameObject.Find("Player").GetComponent<PlayerController>().score;
    }
}
