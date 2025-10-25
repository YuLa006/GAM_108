using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Heart_Item : MonoBehaviour
{     
    [SerializeField] private UI_Manager UI;
    public int Heart =1;
    void Start()
    {
        UI = FindAnyObjectByType<UI_Manager>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Health += Heart;
                UI.UpdateHealth(player.Health);
                Debug.Log(Heart);
            }
            Destroy(gameObject);            
        }        
    }        
}
