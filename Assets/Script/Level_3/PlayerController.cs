using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundCheck G;    
    [SerializeField] private Transform AtkPoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private UI_Manager UI;
    [SerializeField] private GameObject Quest;
    [SerializeField] private AudioSource Audio;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject Intruct;
    public Animator at;

    [SerializeField] private AudioClip PlayerHit;    
    [SerializeField] private AudioClip PlayerAtk;      
    [SerializeField] private AudioClip PlayerDie;

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
    public int countDiamond;
    public int atkDamage = 2;
    public int Health = 1;
    public int maxHealth = 5;
    public float AtkTimer = 1.5f;
    public float AtkCD = 1.5f;
    public int count;

    private const string Level = "HighLevel";
    private const string Diamond = "TotalDiamond";

    public bool Die1Time = false;
    public bool isDie = false;
    public bool isQuest = false;
    public bool isIntruct = false;

    void Start()
    {
        countDiamond = GameObject.FindGameObjectsWithTag("Diamond").Length;       
    }
    void Update()
    {
        //di chuyển trái phải,nhảy,lật player
        if (!isDie)
        {
            MovePlayer();   
        }
        else
        {
            CheckDie();
            inputH = 0;            
        }

        //tấn công
        if (Input.GetKeyDown(KeyCode.J))
        {
            Audio.PlayOneShot(PlayerAtk);
            at.SetTrigger("isAttacking");
            Attack();            
        }

        //Nhấn N để mở nhiệm vụ 
        if (Input.GetKeyDown(KeyCode.N))
        {            
            isQuest = !isQuest;
            Quest.SetActive(isQuest);
        }

        //Nhấn X để mở hướng dẫn
        if (Input.GetKeyDown(KeyCode.X))
        {
            isIntruct = !isIntruct;
            Intruct.SetActive(isIntruct);
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputH * speed, rb.linearVelocityY);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Door") && countDiamond <= UI.Diamond)
        {
            inputH = 0 ;
            rb.linearVelocityY = 0;
            at.SetTrigger("DoorIn");
        }
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
        Audio.PlayOneShot(PlayerHit);

        if (Health <= 0)
        {
            Health = 0;
            isDie = true;            
        }

        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
        UI.UpdateHealth(Health);
        Debug.Log(isDie);                   
    }

    public void CheckDie()
    {
        if (!Die1Time)
        {
            Die1Time = true;
            StartCoroutine(Die());
        }
        IEnumerator Die()
        {
            GameOver.SetActive(true);
            UI.UpdateHighLevelAndTotalDiamond();
            at.SetTrigger("isDie");
            Audio.PlayOneShot(PlayerDie);
            yield return new WaitForSeconds(1f);                    
        }
    }

    public static void SaveHighLevel(int level)
    {
        int line = PlayerPrefs.GetInt(Level, 1);
        if(level > line)
        {
            PlayerPrefs.SetInt(Level, line);
            PlayerPrefs.Save();
        }
    }

    public static int GetHighLevel()
    {
        return PlayerPrefs.GetInt(Level, 1);
    }
    
    public static void AddDiamond(int diamond)
    {
        int total = PlayerPrefs.GetInt(Diamond, 0);
        total += diamond;
        PlayerPrefs.SetInt(Diamond, total);
        PlayerPrefs.Save();
    }

    public static int GetTotalDiamond()
    {
        return PlayerPrefs.GetInt(Diamond, 0);
    }
}
