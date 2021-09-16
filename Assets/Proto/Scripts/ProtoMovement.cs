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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var vel = Input.GetAxisRaw("Horizontal") * moveForce;
        rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, vel, ref refVel, smoothTime), rb.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Pad"))
        {
            rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
        }
    }
}
