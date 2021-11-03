using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class slidermovementtest : MonoBehaviour
{

    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    private float moveSpeed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //[SerializeField] private GameObject player;
    //[SerializeField] private Slider slider;
    //[SerializeField] private float movementSpeedPlayer;

    //public float runSpeed = 10f;

    //float horizontalMove = 0f;
   

    // Update is called once per frame
   //private void Update()
   // {

   //     if (Input.touchCount > 0)
   //     {
   //         Touch touch = Input.GetTouch(0);
   //         touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
   //         direction = (touchPosition - transform.position);
   //         rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;

   //         if (touch.phase == TouchPhase.Ended)
   //             rb.velocity = Vector2.zero;
                
   //     }
        //float movement = slider.value;

        //horizontalMove = slider.Horizontal * runSpeed;


        //player.transform.position = new Vector3(movement, 0, 0) * movementSpeedPlayer * Time.deltaTime;

        //// player faces where moves
        //if(slider.value == 0)
        //{
        //    player.transform.rotation = player.transform.rotation;

        //}
        //else if (slider.value < 0)
        //{
        //    player.transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else if(slider.value > 0)
        //{
        //    player.transform.rotation = Quaternion.Euler(0, 1, 0);
        //}
    }

    //public void resetSlider()
    //{
    //    slider.value = 0;
    //}
}
