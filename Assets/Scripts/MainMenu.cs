﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
