using System;
using UnityEngine;

public class HolePlantation : MonoBehaviour
{
    [SerializeField] private Sprite _emptyHole;
    [SerializeField] private Sprite _holeWithCarrot;
    private SpriteRenderer _spriteBase;

    [SerializeField] private int _health;
    private int _currentHealth;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteBase = GetComponentInChildren<SpriteRenderer>();
        if (_spriteBase == null)
            throw new Exception("O componente 'Sprite Renderer' não foi associado a nenhuma 'child' deste objeto");

        _currentHealth = _health;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_currentHealth <= 0) return;

        if (collider.CompareTag("Shovel"))
            Hitted();
    }

    /// <summary>
    /// Chamada quando o Jogador atinge o chão com a sua pá
    /// </summary>
    private void Hitted()
    {
        _currentHealth--;

        if (_currentHealth <= 0)
            Excavated();
    }

    /// <summary>
    /// Chamada quando os pontos de vida do chão chegam a zero, ou seja, ele foi escavado pelo Jogador
    /// </summary>
    private void Excavated()
    {
       _spriteBase.sprite = _emptyHole;
    }
}
