using System;
using UnityEngine;

public class HolePlantation : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite _emptyHole;
    [SerializeField] private Sprite _holeWithCarrot;
    private SpriteRenderer _spriteBase;

    [Header("Status")]
    [SerializeField] private int _health;
    private int _currentHealth;
    [SerializeField] private float _waterAmount;
    private float _currentWaterAmount;

    public bool FullOfWater { get => _currentWaterAmount == _waterAmount; }
    public bool AvaiableToPlant { get => _currentHealth == 0; }

    private bool _isWeathering;
    private bool _playerInRange;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteBase = GetComponentInChildren<SpriteRenderer>();
        if (_spriteBase == null)
            throw new Exception("O componente 'Sprite Renderer' não foi associado a nenhuma 'child' deste objeto");

        _currentHealth = _health;
        _currentWaterAmount = 0;
    }

    private void Update()
    {
        Weatering();

        GrowUpCarrot();
        CollectCarrot();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _playerInRange = collider.CompareTag("Player");

        var canDig = collider.CompareTag("Shovel") && _currentHealth > 0;
        if (canDig) Dig();

        var canWatering = collider.CompareTag("Watering") && AvaiableToPlant;
        if (canWatering) _isWeathering = true;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _playerInRange = !collider.CompareTag("Player");

        if (collider.CompareTag("Watering"))
            _isWeathering = false;
    }

    /// <summary>
    /// Chamada quando o Jogador atinge o chão com a sua pá
    /// </summary>
    private void Dig()
    {
        _currentHealth--;

        if (_currentHealth <= 0)
            Dug();
    }

    /// <summary>
    /// Chamada quando os pontos de vida do chão chegam a zero, ou seja, ele foi escavado pelo Jogador
    /// </summary>
    private void Dug()
    {
        _spriteBase.sprite = _emptyHole;
    }

    private void Weatering()
    {
        if (_isWeathering)
            _currentWaterAmount += 0.01f;

        if (_currentWaterAmount > _waterAmount)
            _currentWaterAmount = _waterAmount;
    }

    private void GrowUpCarrot()
    {
        if (FullOfWater) _spriteBase.sprite = _holeWithCarrot;
    }

    private void CollectCarrot()
    {
        if (_playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            var playerItems = FindObjectOfType<PlayerItems>();
            if (playerItems.CanCollectCarrot)
            {
                _spriteBase.sprite = _emptyHole;
                _currentWaterAmount = 0;
                playerItems.CollectCarrot();
            }
        }
    }
}
