using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedMovementFinal : MonoBehaviour
{
    private enum Controls { Touch, Motion }
    private Controls controls;

    private Rigidbody2D rb;
    private Animator animator;
    private new SpriteRenderer renderer;

    private float refVel = 0f;
    public float bounceForce;
    public float moveForce;
    public float smoothTime;
    private int input;

    private void Awake()
    {
        controls = PlayerPrefs.GetInt("PogoPlunger_MotionControls", 0) == 1 ? Controls.Motion : Controls.Touch;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch (controls)
        {
            case Controls.Touch:
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, input * moveForce, ref refVel, smoothTime), rb.velocity.y);
                break;

            case Controls.Motion:
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, Input.acceleration.x * 2f * moveForce, ref refVel, smoothTime), rb.velocity.y);
                break;
        }
        
        renderer.flipX = rb.velocity.x < 0 ? true : false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll((Vector2)transform.position - new Vector2(0, 1.1f), new Vector2(0.875f, 0.2f), 0f);
        bool hit = false;
        foreach (Collider2D coll in hits)
        {
            if (coll.transform.CompareTag("Pad") || coll.transform.CompareTag("Floor")) hit = true;
        }

        if (hit)
        {
            if (collision.transform.CompareTag("Pad"))
            {
                rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
            }
            if (collision.transform.CompareTag("Floor"))
            {
                rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
                GameManager.instance.AddScore();
            }

            animator.SetTrigger("Bounce");
        }

    }

    public void Up()
    {
        input = 0;
    }
    public void Left()
    {
        input = -1;
    }
    public void Right()
    {
        input = 1;
    }
}
