using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public void OnHomeButtom()
    {
        SceneManager.LoadScene("Home");
    }

    public void OnLevelsButtom()
    {
        SceneManager.LoadScene("Levels");
    }

    public void OnLevel1Buttom()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnLevel2Buttom()
    {
        SceneManager.LoadScene("Level2");
    }

    public void OnLevel3Buttom()
    {
        SceneManager.LoadScene("Level3");
    }
}
