using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class PigController : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Animator Animator;
    [SerializeField] private Rigidbody2D rg;    
    [SerializeField] private Transform AtkPoint;
    [SerializeField] private LayerMask PlayerLayer;
    
    public float speedPig = 2;
    public int health = 3;
    public int dame = 1;
    public float AtkRange = 0.2f;
    public float AtkCD = 3;
    public float AtkTimer = 3;

    public bool isChasing = false;
    public bool isDie = false;    

    void Start()
    {
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
        
        if (AtkTimer > 0)
        {
            AtkTimer -= Time.deltaTime;            
        }
        else
        {            
            Attack();
        }

        if (isDie)
        {
            takeDamage(1000);
        }
    }

    // Lấy dame của player, set Dead
    public void takeDamage(int dame)
    {
        health -= dame;
        Animator.SetTrigger("isHit");
        if (health <= 0)
        {
            health = 0;
            isDie = true;
            if (isDie)
            {
                Animator.SetTrigger("isDie");
                StartCoroutine(Die());
            }
        }
        IEnumerator Die()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject.Destroy(rg.gameObject);
        }
    }

    // Tấn công player
    public void Attack()
    {
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(AtkPoint.position, AtkRange, PlayerLayer);
        foreach (Collider2D hit in hitplayer)
        {
            StartCoroutine(Attacking(hit));
            AtkTimer = AtkCD;
            break;
        }

        IEnumerator Attacking(Collider2D hit)
        {            
            hit.GetComponent<PlayerController>().TakeDame(dame);
            Animator.SetTrigger("isAttacking");
            yield return new WaitForSeconds(1f);            
        }
    }    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AtkPoint.position, AtkRange);
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
