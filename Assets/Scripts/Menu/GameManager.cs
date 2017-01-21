using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    public static int highscore;
        public gameOver OnGameOver;
        public delegate void gameOver();

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