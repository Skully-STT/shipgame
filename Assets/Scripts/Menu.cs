﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Image fadeScreen;
    public float duration;
    private int levelToLoad;

	public virtual void OnLevelSelect(int _level)
	{
        fadeScreen.color = Color.Lerp(new Color(0,0,0,0), new Color(0, 0, 0, 255),duration);
        levelToLoad = _level;
        Invoke("LoadLevel", duration);
	}
    
    private void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

	public virtual void OnExitClick()
	{
        Debug.Log("EXIT!!!!!11");
        PlayerPrefs.Save();
		Application.Quit();
	}
}
