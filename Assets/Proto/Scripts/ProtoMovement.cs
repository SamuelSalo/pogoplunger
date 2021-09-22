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
        if(collision.transform.CompareTag("Pad"))
        {
            rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
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
