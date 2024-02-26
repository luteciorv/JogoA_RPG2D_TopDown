using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tree : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private int _health;
    [SerializeField] private GameObject _wood;
    [SerializeField] private int _minAmountWood;
    [SerializeField] private int _maxAmountWood;

    private int _currentHealth;
    private int _totalWood;

    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        if (!TryGetComponent(out _animator))
            throw new Exception("O componente 'Animator' não existe neste objeto");

        _currentHealth = _health;
        _totalWood = Random.Range(_minAmountWood, _maxAmountWood + 1);
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

        for(int i = 0; i < _totalWood; i++)
        {
            var newPosition = transform.position + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f));
            Instantiate(_wood,  newPosition, transform.rotation);
        }
    }
}
