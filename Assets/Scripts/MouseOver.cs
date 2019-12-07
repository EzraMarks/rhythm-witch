using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{

    //Background object for sound effect script references
    public WitchSFX WitchSFX;

    // Start is called before the first frame update
    void Start()
    {
        //Find the background spawner object to assign the SFX script
        WitchSFX = GameObject.Find("BackgroundSpawner").GetComponent<WitchSFX>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When the mouse goes over the button, play the little sound effect
    void OnMouseEnter()
    {
        WitchSFX.MenuMove();
    }
}
