using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speedMove=2;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        bc=GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFacingRight())
        {
            rb.velocity = new Vector2(speedMove,0f);
        }else
        {
            rb.velocity = new Vector2(-speedMove,0f);
        }
        
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)),transform.localScale.y);
        }
    }
}
