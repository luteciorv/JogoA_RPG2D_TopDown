using System;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        if (!TryGetComponent(out _animator))
            throw new Exception("O componente Animator não foi associado a este obejto");      
    }

    public void Idle()
        => _animator.SetInteger("transition", 0);

    public void Walk()
        => _animator.SetInteger("transition", 1);

    public void Attack()
        => _animator.SetInteger("transition", 2);

    public void Hit()
        => _animator.SetTrigger("hit");

    public void Die()
    => _animator.SetTrigger("death");
}
