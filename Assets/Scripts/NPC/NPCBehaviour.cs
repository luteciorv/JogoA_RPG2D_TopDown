using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public float MoveSpeed;
    public List<Transform> Paths = new();

    private int _pathIndex;
    private float _currentSpeed;
    private Animator _animator;

    private void Start()
    {
        if (!TryGetComponent(out _animator))
            throw new Exception("Nenhum componente do tipo Animator foi associado a este objeto");

        _currentSpeed = MoveSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (DialogueManager.Instance.WindowVisible)
            Idle();

        else
        {
            Move();
            Rotate();
        }
    }

    private void Idle()
    {
        _animator.SetBool("isWalking", false);
        _currentSpeed = 0;
    }

    private void Move()
    {
        _animator.SetBool("isWalking", true);
        _currentSpeed = MoveSpeed;
        var moveTo = Paths[_pathIndex].position;

        transform.position = Vector2.MoveTowards(transform.position, moveTo, _currentSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveTo) < 0.125f)
            SortNewPath();
    }

    private void SortNewPath()
    {
        _pathIndex = _pathIndex < Paths.Count - 1 ? UnityEngine.Random.Range(0, Paths.Count - 1) : 0;
    }

    private void Rotate()
    {
        var direction = Paths[_pathIndex].position - transform.position;
        if (direction.x > 0)
            transform.eulerAngles = new(0, 0);

        if (direction.x < 0)
            transform.eulerAngles = new(0, 180);
    }
}
