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
    public bool IsDigging { get; private set; }

    public Vector2 Direction { get; private set; }

    private int _currentTool;

    private void Start()
    {
        if (!TryGetComponent(out _rigidBody2D)) 
            throw new Exception("Nenhum RigidBody associado a este componente foi encontrado");

        _currentSpeed = _moveSpeed;
        _currentTool = 1;
    }

    private void Update()
    {
        ChangeTool();

        SetDirection();

        Run();
        Dodge();
        CutTree();
        Dig();
    }

    private void FixedUpdate()
    {
        if (IsCuttingTree || IsDigging) return;

        Move();
    }

    private void SetDirection()
    {
        bool canNotChangeDirection = IsCuttingTree || IsDigging;
        if (canNotChangeDirection) return;

        Direction = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Move() =>
         _rigidBody2D.MovePosition(_rigidBody2D.position + _currentSpeed * Time.fixedDeltaTime * Direction);

    private void Run()
    {
        bool canNotRun = IsCuttingTree || IsDigging || Vector2.zero == Direction;
        if (canNotRun) return;

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
        bool canNotDodge = IsCuttingTree || IsDigging;
        if (canNotDodge) return;

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
        if (_currentTool != 1) return;

        if (Input.GetMouseButtonDown(0))
            IsCuttingTree = true;

        if (Input.GetMouseButtonUp(0))
            IsCuttingTree = false;
    }

    private void Dig()
    {
        if (_currentTool != 2) return;

        if (Input.GetMouseButtonDown(0))
            IsDigging = true;

        if (Input.GetMouseButtonUp(0))
            IsDigging = false;
    }

    private void ChangeTool()
    {
        bool canNotChangeTool = IsCuttingTree || IsDigging;
        if (canNotChangeTool) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            _currentTool = 1;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            _currentTool = 2;
    }
}
