using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player _player;
    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        if (!TryGetComponent(out _player))
            throw new Exception("O componente 'Player' n�o foi encontrado neste objeto");

        if (!TryGetComponent(out _animator))
            throw new Exception("O componente 'Animator' n�o foi encontrado neste objeto");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Run();
        CutTree();
        Dig();

        Flip();
    }

    private void Move()
    {
        // Idle
        if (_player.Direction.sqrMagnitude == 0)
            _animator.SetInteger("transition", 0);

        // Walking e Dodging
        if (_player.Direction.sqrMagnitude == 1)
        {
            if(_player.IsDodging)
                _animator.SetTrigger("dodging");

            else
                _animator.SetInteger("transition", 1);
        }
    }

    private void Run()
    {
        if (_player.IsRunning)
            _animator.SetInteger("transition", 2);
    }

    private void Flip()
    {
        if (_player.Direction.x > 0)
            transform.eulerAngles = new(0, 0);

        else if (_player.Direction.x < 0)
            transform.eulerAngles = new(0, 180);
    }

    private void CutTree()
    {
        if (_player.IsCuttingTree)
            _animator.SetInteger("transition", 3);
    }

    private void Dig()
    {
        if (_player.IsDigging)
            _animator.SetInteger("transition", 4);
    }
}