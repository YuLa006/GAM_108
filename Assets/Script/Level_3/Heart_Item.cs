using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Heart_Item : MonoBehaviour
{     
    [SerializeField] private UI_Manager UI;
    [SerializeField] private PlayerController Player;
    [SerializeField] private Collider2D col;
    public int Heart =1;
    void Start()
    {
        UI = FindAnyObjectByType<UI_Manager>();
        Player = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (Player.Health < 5)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled= false;
        }
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
