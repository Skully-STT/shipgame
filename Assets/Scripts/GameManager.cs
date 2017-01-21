using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}