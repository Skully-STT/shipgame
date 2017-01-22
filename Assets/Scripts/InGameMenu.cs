using System.Collections;

#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : Menu
{
    public static InGameMenu Singleton { get; set; }

    public GameObject gameOver;
    public GameObject exitButtons;
    public GameObject confirm;
    public Text score;
    public Text endScore;
    public Image health;
    public Image lostHealth;
    public Image lowLife;
    public GameObject speed;
    private bool exitClickedOnce = false;
    private bool levelClickedOnce = false;
    public int alarmAktivValue;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            throw new System.InvalidOperationException("Cannot create another instance of the 'IngameMenu' class");
        }
    }

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
        if (ShipManager.Singleton.Shiphealth < alarmAktivValue)
        {
            lowLife.gameObject.SetActive(true);
        }
        health.fillAmount = (float)ShipManager.Singleton.Shiphealth / 100;
        lostHealth.fillAmount = (float)ShipManager.lostHealth / 100;
        speed.transform.rotation = new Quaternion();
        speed.transform.Rotate(new Vector3(0, 0, -(ShipManager.Singleton.Speed * 1.8f)));
        //speedImg.fillAmount = ShipManager.Singleton.Speed / 100;
        //speedTxt.text = ShipManager.Singleton.Speed.ToString()+"%";
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.Escape))
        {
            GameOver();
        }
#endif
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
        SceneManager.LoadScene(0);
    }

    public override void OnLevelSelect(int level)
    {
        if (levelClickedOnce == true)
        {
            Time.timeScale = 1;
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