﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
