using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    public bool passable = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player") && !passable)
        {
            GameManager.instance.Death();
            transform.parent.GetComponent<PlatformGeneration>().CreatePlatform();
            passable = true;
        }
    }
}
