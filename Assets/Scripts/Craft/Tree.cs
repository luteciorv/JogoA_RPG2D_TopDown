using System;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int Health;

    private int _currentHealth;

    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        if (!TryGetComponent(out _animator))
            throw new Exception("O componente 'Animator' não existe neste objeto");

        _currentHealth = Health;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Axe"))
            Hitted();
    }

    /// <summary>
    /// Chamada quando o Jogador atinge a árvore com o seu machado
    /// </summary>
    private void Hitted()
    {
        _currentHealth--;
        _animator.SetTrigger("Hitted");

        if (_currentHealth <= 0)
            Cutted();
    }

    /// <summary>
    /// Chamada quando os pontos de vida da árvore chegam a zero, ou seja, ela foi cortada pelo Jogador
    /// </summary>
    private void Cutted()
    {
        _animator.SetTrigger("Cutted");
    }
}
