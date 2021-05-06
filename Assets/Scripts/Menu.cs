using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button[] Levels;
    //public Text coinsUI;
    public Text coinsTotalUI;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            for (int i = 0; i < Levels.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("Level"))
                    Levels[i].interactable = true;
                else
                    Levels[i].interactable = false;
            }
        }
        else
        {
            Levels[0].interactable = true;
            for (int i = 1; i < Levels.Length; i++)
                Levels[i].interactable = false;
        }
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Coins"))
            coinsTotalUI.text = PlayerPrefs.GetInt("Coins").ToString();
        else
            coinsTotalUI.text = "0";
    }

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void DelKeys()
    {
        PlayerPrefs.DeleteAll();
    }
}
