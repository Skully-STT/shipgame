﻿using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Singleton { get; set; }

    public static int highscore;

    public void Awake()
    {
	    if (Singleton == null)
	    {
		    Singleton = this;
	    }
	    else
	    {
		    throw new System.InvalidOperationException("Cannot create another instance of the 'GameManager' class");
	    }
    }

    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}