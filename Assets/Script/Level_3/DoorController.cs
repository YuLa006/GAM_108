using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string SceneName;
    [SerializeField] private AudioSource Audio;    

    [SerializeField] private AudioClip AudioVictory;
    public int Diamond;    
    public UI_Manager UI;
    public int CountLevel;    

    public bool isOpen;

    private void Start()
    {
        Diamond = GameObject.FindGameObjectsWithTag("Diamond").Length;
    }
    private void Update()
    {        
        if (Diamond <= UI.Diamond && !isOpen)
        {
            isOpen = true;
            CheckOpenDoor();            
        }
    } 

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(Diamond <= UI.Diamond && col.CompareTag("Player"))
        {            
            StartCoroutine(CheckDoor());
            IEnumerator CheckDoor()
            {
                PlayerController.SaveHighLevel(CountLevel);
                PlayerController.AddDiamond(Diamond);                

                Audio.PlayOneShot(AudioVictory);
                animator.SetTrigger("isClose");                
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneName);
            }            
        }
    }

    public void CheckOpenDoor()
    {
        StartCoroutine(Open());
        IEnumerator Open()
        {
            isOpen = true;
            animator.SetTrigger("isOpen");
            yield return new WaitForSeconds(1f);
        }
    }   
}
