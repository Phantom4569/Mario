using UnityEngine;
using UnityEngine.SceneManagement;

public class levels : MonoBehaviour
{
    public void Loadlevel1()
    {
        SceneManager.LoadScene("lvl1");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("menu");
    }
    public void Loadlevel2()
    {
        SceneManager.LoadScene("lvl2");
    }
}
