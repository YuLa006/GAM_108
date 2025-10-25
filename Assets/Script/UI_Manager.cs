using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] SmallHeart;    
    public TextMeshProUGUI DiamondText;
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
        DiamondText.text =  Diamond.ToString();
    }       
}
