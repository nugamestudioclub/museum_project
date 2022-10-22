using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private CharacterController controller;
    public Vector3 playerVelocity;
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float gravity = -10f;

    private Transform groundCheck;
    private float groundDistance = 0.5f;
    public LayerMask groundMask;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        groundCheck = transform.Find("GroundCheck").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        
        // wasd player movement
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 xzMove = (transform.right * xMove + transform.forward * zMove) * moveSpeed;

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // final player move
        controller.Move((playerVelocity + xzMove) * Time.deltaTime);

        //Debug.Log(controller.isGrounded);
    }
}