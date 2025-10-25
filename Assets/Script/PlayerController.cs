using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Heart_Item Heart_Item;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundCheck G;
    [SerializeField] private Animator at;
    [SerializeField] private Transform AtkPoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private UI_Manager UI;
    [SerializeField] private GameObject Diamond;

    [Header("Cấu hình nhân vật")]
    [Tooltip("Tốc độ của nhân vật")]
    public float speed = 5;
    [Tooltip("Di chuyển trái phải")]
    public float inputH;
    [Tooltip("Tốc độ nhảy")]
    public float SpeedJump = 12;
    [Tooltip("Bán kính AtkPiont")]
    public float AtkRange = 0.55f;
    [Tooltip("Damage của player")]
    public int countHealth = 0;
    public int countDiamond = 0;
    public int atkDamage = 2;
    public int Health = 1;
    public int maxHealth = 5;
    public float AtkTimer = 1.5f;
    public float AtkCD = 1.5f;
    public int count;

    public bool isDie = false;

    void Start()
    {
        //rb = GetComponent <Rigidbody2D>();  
        //sr = GetComponent<SpriteRenderer>();     
        //at = GetComponent <Animator>();        
    }
    void Update()
    {

        //di chuyển trái phải,nhảy,lật player
        if (!isDie)
        {
            MovePlayer();
        }

        //tấn công
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
            at.SetTrigger("isAttacking");
        }

    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputH * speed, rb.linearVelocityY);
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AtkPoint.position, AtkRange, enemyLayer);
        foreach (Collider2D hit in hitEnemies)
        {
            hit.GetComponent<PigController>().takeDamage(atkDamage);
            AtkTimer = AtkCD;
            StartCoroutine(Attacking());
            break;
        }

        IEnumerator Attacking()
        {
            at.SetTrigger("isAttcking");
            yield return new WaitForSeconds(1f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AtkPoint.position, AtkRange);
    }

    void MovePlayer()
    {
        // di chuyển trái phải
        inputH = Input.GetAxisRaw("Horizontal");

        // Nhảy
        if (G.isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, SpeedJump);
            at.SetBool("isJumping", true);
        }
        else
        {
            at.SetBool("isJumping", false);
        }

        // lật trái phải 
        if (inputH > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            at.SetBool("isRuning", true);
        }
        else if (inputH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            at.SetBool("isRuning", true);
        }
        else
        {
            at.SetBool("isRuning", false);
        }
    }

    public void TakeDame(int dame)
    { 
        Health -= dame;
        at.SetTrigger("isHit");     

        if (Health <= 0)
        {
            Health = 0;
            isDie = true;
            at.SetTrigger("isDie");
            StartCoroutine(Die());
        }

        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
        UI.UpdateHealth(Health);

        IEnumerator Die()
        {
            yield return new WaitForSeconds(0.5f);

        }              
    }
    
}
