using UnityEngine;

public class SawTrap : MonoBehaviour
{
    public Transform Saw;
    public float speedSaw;

    public bool CheckGround;

    private void FixedUpdate()
    {
        if (CheckGround)
        {
            transform.Translate(Vector3.up * speedSaw * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * speedSaw * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            CheckGround = !CheckGround;
        }

        if (col.CompareTag("Player"))
        {
            PlayerController player = col.GetComponent<PlayerController>();
            player.isDie = true;
        }
    }
}
