using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] private PlayerInput _input;
    private readonly string _moveInput = "Move";
    private readonly string _jumpInput = "Jump";
    private InputAction _move;
    private InputAction _jump;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _input = GetComponent<PlayerInput>();

        _move = _input.actions[_moveInput];
        _jump = _input.actions[_jumpInput];
        
        _jump.performed += ctx => Jump();
    }

    void Jump(){
        // _rb.AddForce(new Vector2(0, _jumpPower));
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveDirection = _move.ReadValue<Vector2>();

        float moveX = moveDirection.x;
        
        _rb.velocity = new Vector2(moveX*_speed, _rb.velocity.y);
    }
}
