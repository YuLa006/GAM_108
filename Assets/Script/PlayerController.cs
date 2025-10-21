using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundCheck G;
    [SerializeField] private Animator at;
    [SerializeField] private Collider2D col;
    [SerializeField] private Transform AtkPoint;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Cấu hình nhân vật")]
    [Tooltip("Tốc độ của nhân vật")]
    public float speed;
    [Tooltip("Di chuyển trái phải")]
    public float inputH;
    [Tooltip("Tốc độ nhảy")]
    public float SpeedJump;
    [Tooltip("Bán kính AtkPiont")]
    public float AtkRange;
    [Tooltip("Damage của player")]
    public float atkDamage;


    void Start()
    {
        rb = GetComponent <Rigidbody2D>();  
        sr = GetComponent<SpriteRenderer>();     
        at = GetComponent <Animator>();
    }
    void Update()
    {        
        //di chuyển trái phải,nhảy,lật player
        MovePlayer();
        
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputH * speed,rb.linearVelocityY) ;
    }
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AtkPoint.position, AtkRange, enemyLayer);
        foreach (Collider2D hit in hitEnemies)
        {
            hit.GetComponent<PigController>();
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

        //tấn công

    }
}
