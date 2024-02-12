using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public PlayerHealth playerhealth;
   

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Luffy")
        {
            playerhealth.TakeDamage(damage);
        }
    }
}
