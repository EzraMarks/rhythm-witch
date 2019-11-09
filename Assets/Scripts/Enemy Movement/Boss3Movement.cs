using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss3Movement : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        if (GameObject.Find("Player").GetComponent<DodgeCollision>().Health > 0)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
