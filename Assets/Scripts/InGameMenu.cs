using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : Menu
{
    public GameObject exitButtons;
    public GameObject confirm;
    public Text score;
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
        health.fillAmount = (float)ShipManager.shipHealth / 100;
        lostHealth.fillAmount = (float)ShipManager.lostHealth / 100;
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


    public override void OnLevelSelect(int level)
    {
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
