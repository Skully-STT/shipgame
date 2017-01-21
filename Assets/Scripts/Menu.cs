using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public virtual void OnLevelSelect(int level)
	{
		SceneManager.LoadScene(level);
	}

	public virtual void OnExitClick()
	{
        Debug.Log("EXIT!!!!!11");
        PlayerPrefs.Save();
		Application.Quit();
	}
}
