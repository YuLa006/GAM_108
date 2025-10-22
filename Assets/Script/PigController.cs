using System;
using System.Collections;
using System.Collections.Generic;
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
    public float health;    

    public bool isDie = false;


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

    // Lấy dame của player, set Dead
    public void takeDamage(float dame)
    {
        health -= dame;
        if (health <= 0)
        {
            health = 0;
            isDie = true;
            Animator.SetTrigger("isDie");
            StartCoroutine(Die());
        }
        IEnumerator Die()
        {
            yield return new WaitForSeconds(1f);
            GameObject.Destroy(rg.gameObject);
        }
    }
        
    //Kiểm tra biến chase
    public void SetChasing(bool vaule)
    {
        isChasing = vaule;
    }

    // đuổi theo Player
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
            transform.localScale = new Vector3(-1, 1, 1);    
        }
        else if(direction.x < 0){
            transform.localScale = new Vector3(1, 1, 1);
        }        
        direction.Normalize();

        rg.linearVelocity = direction * speedPig;
        Animator.SetBool("isRunning", true);
    }
}
