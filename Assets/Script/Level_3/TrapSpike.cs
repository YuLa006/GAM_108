using Unity.VisualScripting;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerController player = col.GetComponent<PlayerController>();
            player.isDie = true;            
        }

        if (col.CompareTag("Pig"))
        {
            PigController Pig = col.GetComponent<PigController>();
            Pig.isDie = true;
        }
    }            
}
