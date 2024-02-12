using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator an;
    private bool isGrounded;
    private float dirX = 0f;
    private SpriteRenderer sp;

    [SerializeField]
    private float JumpForce = 12f;

    [SerializeField]
    private float PushForce = 12f;

    [SerializeField]
    private float MoveSpeed = 7f;

    [SerializeField]
    private LayerMask jumpableGround;

    [SerializeField]
    private Anchor selectedAnchor;

    private enum MovemenetState
    {
        idle,
        running,
        jump,
        fall
    };

    private LineRenderer lineRend;
    private DistanceJoint2D distJoin;

    [SerializeField]
    private AudioSource jumpSoundEffect;
    float a;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        isGrounded = false;
        an = GetComponent<Animator>();
        lineRend = GetComponent<LineRenderer>();
        distJoin = GetComponent<DistanceJoint2D>();

        lineRend.enabled = false;
        distJoin.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(dirX * MoveSpeed - rb.velocity.x, 0), ForceMode2D.Impulse);

        if (Input.GetButtonDown("Jump") && InGround())
        {
            if (isGrounded)
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                SpecialEffects.specialEffects.CreateSmoke(transform);
            }
        }

        UpdateAnimationState();
        if (Input.GetKeyDown("f") && distJoin.enabled == false)
        {
            AnchorBehaviour();
        }
        else if (Input.GetKeyDown("f") && distJoin.enabled == true)
        {
            lineRend.enabled = false;
            distJoin.enabled = false;
            DeselectAnchor();
        }

        if (distJoin.enabled == true)
        {
            rb.mass = 15;
            dirX = Input.GetAxisRaw("Horizontal");
            rb.AddForce(new Vector2(dirX * 14 - rb.velocity.x, 0), ForceMode2D.Impulse);
        }

        if (lineRend.enabled)
        {
            UpdateLineRenderer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void UpdateAnimationState()
    {
        MovemenetState state;
        if (dirX > 0f)
        {
            state = MovemenetState.running;
            sp.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovemenetState.running;
            sp.flipX = true;
        }
        else
        {
            state = MovemenetState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovemenetState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovemenetState.fall;
        }

        an.SetInteger("state", (int)state);
    }

    private bool InGround()
    {
        return Physics2D.BoxCast(
            coll.bounds.center,
            coll.bounds.size,
            0f,
            Vector2.down,
            .1f,
            jumpableGround
        );
    }

    private void SelectAnchor(Anchor anchor)
    {
        selectedAnchor = anchor;
        Debug.Log("hi");
    }

    private void DeselectAnchor()
    {
        rb.mass = 1;
        Vector2 detachDirection = (
            transform.position - selectedAnchor.transform.position
        ).normalized;

        Vector2 pushVelocity = new Vector2(0f, PushForce);

        rb.velocity = pushVelocity;
    }

    private void AnchorBehaviour()
    {
        if (Vector2.Distance(selectedAnchor.transform.position, transform.position) < 4.1f)
        {
            distJoin.enabled = true;
            lineRend.enabled = true;
            distJoin.connectedBody = selectedAnchor.GetComponent<Rigidbody2D>();
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, selectedAnchor.transform.position);
        }
    }

    private void UpdateLineRenderer()
    {
        lineRend.SetPosition(0, transform.position);
        lineRend.SetPosition(1, selectedAnchor.transform.position);
    }
}
