using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss3Movement : MonoBehaviour
{

    bool bossgothurt;

    // Start is called before the first frame update
    void Start()
    {
        bool bossgothurt = GameObject.Find("Player").GetComponent<PlayerController>().bossgothurt;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        bool bossgothurt = GameObject.Find("Player").GetComponent<PlayerController>().bossgothurt;
        if (bossgothurt == true && GameObject.Find("Player").GetComponent<DodgeCollision>().Health > 0)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
