using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player _player;
    private Fishing _fishingArea;
    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        if (!TryGetComponent(out _player))
            throw new Exception("O componente 'Player' não foi encontrado neste objeto");

        if (!TryGetComponent(out _animator))
            throw new Exception("O componente 'Animator' não foi encontrado neste objeto");

        _fishingArea = FindObjectOfType<Fishing>();
        if (_fishingArea == null) throw new Exception("O sript não foi encontrado em cena");
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Run();
        CutTree();
        Dig();
        Watering();

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

    private void Watering()
    {
        if (_player.IsWatering)
            _animator.SetInteger("transition", 5);
    }


    public void Fishing()
    {
        _animator.SetTrigger("fishing");
        _player.Fishing();
    }

    public void EndFishing()
    {
        _fishingArea.CatchFish();
        _player.EndFishing();
    }

    public void Building()
    {
        _animator.SetBool("building", true);
        _player.Building();
    }

    public void EndBuild()
    {
        _animator.SetBool("building", false);
        _player.EndBuilding();
    }

    public void Hit()
    {
        _animator.SetTrigger("hit");
    }
}
