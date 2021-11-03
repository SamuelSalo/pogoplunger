using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motion_movement : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Vector2 movement;
    public float movementSpeed = 10f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = new Vector2(Input.acceleration.x, 0f) * movementSpeed;
        rigid.AddForce(movement);
    }


    //Rigidbody2D rb;
    //float dirx;
    //float moveSpeed = 20f;

    //void Start()

    //{
    //    rb = GetComponent<Rigidbody2D>();

    //}

    //void Update()
    //{
    //    dirx = Input.acceleration.x * moveSpeed;
    //    transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);
    //}

    //void FixedUpdate()
    //{
    //    rb.velocity = new Vector2(dirx, 0f);
    //}
}

