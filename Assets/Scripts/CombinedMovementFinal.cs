using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedMovementFinal : MonoBehaviour
{
    private enum Controls { Touch = 0, Drag = 1, Motion = 2 }
    private Controls controls;

    private Rigidbody2D rb;
    private Animator animator;
    private new SpriteRenderer renderer;

    private float refVel = 0f;
    public float bounceForce;
    public float moveForce;
    public float smoothTime;
    private int input;
    private bool frozen = false;
    private GameObject splashPrefab;

    private void Awake()
    {
        controls = (Controls)PlayerPrefs.GetInt("PogoPlunger_MotionControls", 0);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        splashPrefab = Resources.Load<GameObject>("Splash");
    }

    private void Update()
    {
        if (frozen) return;

        switch (controls)
        {
            case Controls.Touch:
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, input * moveForce, ref refVel, smoothTime), rb.velocity.y);
                break;

            case Controls.Motion:
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, Input.acceleration.x * 2f * moveForce, ref refVel, smoothTime), rb.velocity.y);
                break;
            case Controls.Drag:
                var charPosition = Camera.main.WorldToScreenPoint(transform.position);
                Vector2 touchPosition = charPosition;

#if UNITY_ANDROID
                if(Input.touches.Length > 0)
                    touchPosition = Input.GetTouch(0).position;
#endif
#if UNITY_EDITOR_WIN
                if(Input.GetMouseButton(0))
                    touchPosition = Input.mousePosition;
#endif
                
                var direction =  touchPosition - (Vector2)charPosition;
                direction.x = Mathf.Clamp(direction.x, -50f, 50f);
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, (direction.x / 50f) * moveForce, ref refVel, smoothTime), rb.velocity.y);
                break;
        }

        renderer.flipX = rb.velocity.x < 0;
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
                var splash = Instantiate(splashPrefab, collision.GetContact(0).point, Quaternion.identity);
                Destroy(splash, 0.183f);
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


    public void Death()
    {
        StartCoroutine(DeathRoutine());
    }
    public void Revive()
    {
        animator.SetTrigger("Revive");
        frozen = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator DeathRoutine()
    {
        frozen = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
        GameManager.instance.Death();
    }
}
