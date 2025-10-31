using System.Collections;
using UnityEngine;

public class DiamondItem : MonoBehaviour
{
    [SerializeField] private UI_Manager UI;    

    void Start()
    {
         UI = FindAnyObjectByType<UI_Manager>();        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            UI.UpdateDiamond();
            GameObject.Destroy(gameObject);
        }
    }
    
}
