using System;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed;

    private Rigidbody2D _rigidBody2D;
    public Vector2 Direction { get; private set; }

    private void Start()
    {
        if (!TryGetComponent(out _rigidBody2D)) 
            throw new Exception("Nenhum RigidBody associado a este componente foi encontrado");
    }

    private void Update()
    {
        Direction = GetDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private Vector2 GetDirection() =>
        new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    private void Move() =>
         _rigidBody2D.MovePosition(_rigidBody2D.position + Direction * MoveSpeed * Time.fixedDeltaTime);
}
