using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Player player;
    public Text coinsUI;
    public Text timeUI;
    public Image[] heartsUI;
    public Sprite isLife, noLife;
    public GameObject PauseScreen;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    float levelTime = 0f;
    public TimeWork timeWork;
    public float countdown = 60;

    public void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        if ((int)timeWork == 2)
            levelTime = countdown;
    }

    private void Update()
    {
        coinsUI.text = player.GetCoins().ToString();

        for (int i = 0; i < heartsUI.Length; i++)
        {
            if (player.GetHealth() > i)
                heartsUI[i].sprite = isLife;
            else
                heartsUI[i].sprite = noLife;
        }


        if ((int)timeWork == 1)
        {
            levelTime += Time.deltaTime;
            timeUI.text = ShowGameTime(levelTime);
        }
        else
        if ((int)timeWork == 2)
        {
            levelTime -= Time.deltaTime;
            timeUI.text = ShowGameTime(levelTime);
            if (levelTime <= 0)
                YouLose();
        }
        else
            timeUI.gameObject.SetActive(false);
    }

    public string ShowGameTime(float gameTime)
    {
        int minutes = (int)gameTime / 60;
        int seconds = (int)gameTime - ((int)gameTime / 60) * 60;

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        PauseScreen.SetActive(true);
    }

    public void PauseOff()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        PauseScreen.SetActive(false);
    }

    public void YouWin()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        WinScreen.SetActive(true);

        if (!PlayerPrefs.HasKey("Level") || PlayerPrefs.GetInt("Level") < SceneManager.GetActiveScene().buildIndex)
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);

        if (!PlayerPrefs.HasKey("Coins"))
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + player.GetCoins());
        else
            PlayerPrefs.SetInt("Coins", player.GetCoins());
    }

    public void YouLose()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        LoseScreen.SetActive(true);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}


public enum TimeWork
{
    None,
    Stopwatch,
    Timer
}