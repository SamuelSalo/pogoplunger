using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Break");
            Destroy(gameObject, 0.3f);
        }
    }
}