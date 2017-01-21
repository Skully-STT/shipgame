using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : Menu
{
    public GameObject gameOver;
    public GameObject exitButtons;
    public GameObject confirm;
    public Text score;
    public Text endScore;
    public Image health;
    public Image lostHealth;
    private bool exitClickedOnce = false;
    private bool levelClickedOnce = false;

    public void Start()
    {
#if UNITY_EDITOR
        if (!FindObjectOfType<ShipManager>())
        {
            Debug.Log("NO ShipManager................................................................................................................................................................Idiot!");
            EditorApplication.isPaused = true;
        }
#endif
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            exitButtons.SetActive(!exitButtons.activeInHierarchy);
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }
        score.text = GameManager.highscore.ToString();
        endScore.text = GameManager.highscore.ToString();
        health.fillAmount = (float)ShipManager.Singleton.Shiphealth / 100;
        lostHealth.fillAmount = (float)ShipManager.lostHealth / 100;
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.Escape))
        {
            GameOver();
        }
#endif
    }

    public void OnEnable()
    {
        GameManager.Singleton.OnGameOver += GameOver;
    }

    public void OnDisable()
    {
        GameManager.Singleton.OnGameOver -= GameOver;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public override void OnExitClick()
    {
        if (exitClickedOnce)
        {
            base.OnExitClick();
        }
        else
        {
            confirm.SetActive(true);
            StartCoroutine(CloseConfirm());
            exitClickedOnce = true;
        }
    }

    private IEnumerator CloseConfirm()
    {
        yield return new WaitForSecondsRealtime(2f);
        confirm.SetActive(false);
        exitClickedOnce = false;
        levelClickedOnce = false;
    }

    public void LoadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public override void OnLevelSelect(int level)
    {
        Time.timeScale = 1;
        if (levelClickedOnce == true)
        {
            base.OnLevelSelect(level);
        }
        else
        {
            confirm.SetActive(true);
            StartCoroutine(CloseConfirm());
            levelClickedOnce = true;
        }
    }
}
