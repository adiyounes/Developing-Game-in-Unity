using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int healthmax=10;
    public int health;
    
    [SerializeField] private AudioSource deathSoundEffect;
    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
        
    {
        health=healthmax;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health-=damage;
        if(health<=0)
        {
            deathSoundEffect.Play();
            dead();
            Invoke("reload",2f);
        }
    }

    public void dead()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");

        
    }

    public void reload()
    {
        SceneManager.LoadScene(4);
    } 
}
