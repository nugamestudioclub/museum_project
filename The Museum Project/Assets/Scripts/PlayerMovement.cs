using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 2f;
    [SerializeField]
    private float sprintSpeed = 10f;
    private float moveSpeed;
    private float sprintBonus;

    private CharacterController controller;
    public Vector3 playerVelocity;
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float gravity = -10f;

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

        moveSpeed = walkSpeed;
        sprintBonus = sprintSpeed - walkSpeed;
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

        // sprint mechanic
        float sprintInput = Input.GetAxis("Sprint");
        // wasd player movement
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 xzMove = (transform.right * xMove + transform.forward * zMove) * (moveSpeed + sprintInput * sprintBonus);

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