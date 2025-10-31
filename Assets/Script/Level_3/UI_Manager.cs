using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] SmallHeart;
    [SerializeField] private AudioSource Audio;

    [SerializeField] private AudioClip AudioDiamond;
    public TextMeshProUGUI DiamondText;    
    public TextMeshProUGUI HighLevel;
    public TextMeshProUGUI TotalDiamondText;

    public int maxHeart = 5;
    public int Diamond = 0;
       
    public void UpdateHealth(int health)
    {        
        for (int i = 0; i < maxHeart; i++)
        {
            SmallHeart[i].SetActive(i < health);
        }
    }    
    public void UpdateDiamond()
    {        
        Diamond += 1;
        Audio.PlayOneShot(AudioDiamond);
        DiamondText.text = Diamond.ToString();
        StartCoroutine(WaitAudio());                
    }  
    
    private IEnumerator WaitAudio()
    {
        yield return new WaitForSeconds(1f); 
    }

    public void UpdateHighLevelAndTotalDiamond()
    {
        int level = PlayerController.GetHighLevel();
        HighLevel.text = level.ToString();

        int diamond = PlayerController.GetTotalDiamond();
        TotalDiamondText.text = diamond.ToString();
    }
}
