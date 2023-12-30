using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject shopScreen;
    public Text text;
    public Text text2;
    private int coins;

    private void Update()
    {
        int coins = PlayerPrefs.GetInt("coins");
        text.text = PlayerPrefs.GetInt("coins").ToString();
        text2.text = PlayerPrefs.GetInt("bullets").ToString();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Game levels");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void LoadShop()
    {
        shopScreen.SetActive(true);
    }
    public void UnLoadShop()
    {
        shopScreen.SetActive(false);
    }
    public void BuyBullets10q()
    {
        if (PlayerPrefs.GetInt("coins") >= 20)
        {
            PlayerPrefs.SetInt("bullets", PlayerPrefs.GetInt("bullets") + 10);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 20);
        }
    }
    public void BuyBullets50q()
    {
        if (PlayerPrefs.GetInt("coins") >= 90)
        {
            PlayerPrefs.SetInt("bullets", PlayerPrefs.GetInt("bullets") + 50);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 90);
        }
    }

}