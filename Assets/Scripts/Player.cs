using System;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed;

    private Rigidbody2D _rigidBody2D;
    private Vector2 _direction;

    private void Start()
    {
        if (!TryGetComponent(out _rigidBody2D)) 
            throw new Exception("Nenhum RigidBody associado a este componente foi encontrado");
    }

    private void Update()
    {
        _direction = GetDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private Vector2 GetDirection() =>
        new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    private void Move() =>
         _rigidBody2D.MovePosition(_rigidBody2D.position + _direction * MoveSpeed * Time.fixedDeltaTime);
}
