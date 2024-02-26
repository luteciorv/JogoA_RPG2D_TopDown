using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;

    private float _currentSpeed;

    private Rigidbody2D _rigidBody2D;

    public bool IsRunning { get; private set; }
    public bool IsDodging { get; private set; }
    public bool IsCuttingTree { get; private set; }
    public Vector2 Direction { get; private set; }

    private void Start()
    {
        if (!TryGetComponent(out _rigidBody2D)) 
            throw new Exception("Nenhum RigidBody associado a este componente foi encontrado");

        _currentSpeed = _moveSpeed;
    }

    private void Update()
    {
        SetDirection();
        Run();
        Dodge();
        CutTree();
    }

    private void FixedUpdate()
    {
        if (IsCuttingTree) return;

        Move();
    }

    private void SetDirection()
    {
        if (IsCuttingTree) return;

        Direction = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Move() =>
         _rigidBody2D.MovePosition(_rigidBody2D.position + Direction * _currentSpeed * Time.fixedDeltaTime);

    private void Run()
    {
        if (IsCuttingTree) return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsRunning = true;
            _currentSpeed = _runSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsRunning = false;
            _currentSpeed = _moveSpeed;
        }
    }

    private void Dodge()
    {
        if (IsCuttingTree) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsDodging = true;
            _currentSpeed = _runSpeed;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            IsDodging = false;
            _currentSpeed = _moveSpeed;
        }
    }

    private void CutTree()
    {
        if (Input.GetMouseButtonDown(1))
            IsCuttingTree = true;

        if (Input.GetMouseButtonUp(1))
            IsCuttingTree = false;
    }
}
