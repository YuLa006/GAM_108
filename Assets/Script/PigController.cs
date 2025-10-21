using System;
using UnityEditor.Rendering;
using UnityEngine;

public class PigController : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Animator Animator;
    [SerializeField] private Rigidbody2D rg;
    [SerializeField] private SpriteRenderer sp;

    public bool isChasing = false;
    public float speedPig;


    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
   
    void Update()
    {
        if (isChasing)
        {
            chasePlayer();
        }
        else
        {
            Animator.SetBool("isRunning", false);
        }        
    }
    void takeDamage()
    {

    }
    
    public void SetChasing(bool vaule)
    {
        isChasing = vaule;
    }

    private void chasePlayer()
    {
        if (Player == null)
        {
            Console.WriteLine("Không tìm thấy player");
            return;
        }

        Vector2 direction = Player.position - transform.position;
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);    
        }
        else if(direction.x < 0){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        direction.Normalize();
        rg.linearVelocity = direction * speedPig;
        Animator.SetBool("isRunning", true);
    }
}
