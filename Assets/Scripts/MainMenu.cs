using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Background object for sound effect script references
    public WitchSFX WitchSFX;
    //Main menu music object
    public GameObject menumusic;

    // Start is called before the first frame update
    void Start()
    {
        //Find the background spawner object to assign the SFX script
        WitchSFX = GameObject.Find("BackgroundSpawner").GetComponent<WitchSFX>();
        //Find the menu music object
        menumusic = GameObject.Find("MenuMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        Destroy(menumusic);
        WitchSFX.StartButton();
        SceneManager.LoadScene("LevelScene");
    }

    public void Instructions()
    {
        WitchSFX.ButtonForward();
        SceneManager.LoadScene("InstructionsScene");
    }

    public void Credits()
    {
        WitchSFX.ButtonForward();
        SceneManager.LoadScene("NewCreditsScene");
    }

    public void GameQuit()
    {
        WitchSFX.ButtonBackward();
        Application.Quit();
    }

    public void MouseOver()
    {
        WitchSFX.MenuMove();
    }
}
