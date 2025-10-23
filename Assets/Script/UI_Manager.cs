using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] SmallHeart;    

    public float maxHeart = 5;

    public void UpdateHealth(int health)
    {
        for (int i = 0; i < maxHeart; i++)
        {
            SmallHeart[i].SetActive(i < health);
        }
    }
    
}
