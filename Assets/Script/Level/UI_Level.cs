using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Level : MonoBehaviour
{
    public void OnButtomHome()
    {
        SceneManager.LoadScene("Home");
    }
    public void Level_1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level_2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level_3()
    {
        SceneManager.LoadScene("Level3");
    }
}
