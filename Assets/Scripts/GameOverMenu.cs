﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
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

    public void MainMenu()
    {
        WitchSFX.ButtonBackward();
        Destroy(GameObject.Find("Scorekeeper"));
        SceneManager.LoadScene("MenuScene");
    }

    public void GameQuit()
    {
        WitchSFX.ButtonBackward();
        Application.Quit();
    }

    public void Retry()
    {
        WitchSFX.StartButton();
        Destroy(GameObject.Find("Scorekeeper"));
        SceneManager.LoadScene("LevelScene");
    }

    public void MouseOver()
    {
        WitchSFX.MenuMove();
    }
}
