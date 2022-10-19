using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float jumpHeight = 1f;
    private float gravity = -1f;
    private bool groundedPlayer;


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // wasd movement mechanics
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * xMove + transform.forward * zMove;

        controller.Move(movement * moveSpeed * Time.deltaTime);

        /*
        // player grounding
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // jump mechanic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space pressed");
            Debug.Log(controller.isGrounded);
        }
        else
        {
            Debug.Log(controller.isGrounded);
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            Debug.Log("jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
        */

        // gravity mechanic
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }
}
