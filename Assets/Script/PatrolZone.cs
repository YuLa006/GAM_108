using UnityEngine;

public class PatrolZone : MonoBehaviour
{
    [SerializeField] private PigController pig;              

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
