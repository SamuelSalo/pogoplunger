using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.CompareTag("Player"))
        {
            col.gameObject.GetComponent<CombinedMovementFinal>().Death();

        }
    }
}
