using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private float _jumpHeight = 1f;
    private float _gravity = -1f;
    private bool _groundedPlayer;


    // Start is called before the first frame update
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Input.GetAxis("Horizontal") * transform.right * moveSpeed + Input.GetAxis("Vertical")* transform.forward * moveSpeed;
        print(move);
        _groundedPlayer = _controller.isGrounded;
        if(_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
        // jump
        if(Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
        }
        _playerVelocity.y += _gravity * Time.deltaTime;
        _controller.Move((_playerVelocity+move) * Time.deltaTime);
    }
}