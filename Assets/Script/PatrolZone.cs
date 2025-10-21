using UnityEngine;

public class PatrolZone : MonoBehaviour
{
    [SerializeField] private PigController pig;
        
    
    void Start()
    {
        
    }
 
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            pig.SetChasing(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            pig.SetChasing(false);
        }
    }
}
