using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float refVel = 0f;
    public float bounceForce;
    public float moveForce;
    public float smoothTime;
    public int input;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, input * moveForce, ref refVel, smoothTime), rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll((Vector2)transform.position - new Vector2(0, 0.8f), new Vector2(0.875f, 0.2f), 0f);
        bool hit = false;
        foreach(Collider2D coll in hits)
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
