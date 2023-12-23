using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void ReloadLevel()
    {
        SceneManager.LoadScene("lvl1");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("lvl1");
        }
    }
}