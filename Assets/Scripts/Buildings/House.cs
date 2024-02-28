using System;
using UnityEngine;

public class House : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _playerBuildPosition;
    [SerializeField] private SpriteRenderer _spriteBase;
    [SerializeField] private GameObject _houseCollider;

    [Header("Settings")]
    [SerializeField] private float _timeToBuild;
    [SerializeField] private int _woodCost;

    private float _currentTimeToBuild;
    
    private bool _startBuilding;
    private bool _builted;

    private Color32 _startColor;
    private Color32 _endColor;

    private bool _playerIn;
    private PlayerItems _playerItems;
    private PlayerAnimation _playerAnimation;

    private void Start()
    {
        _houseCollider.SetActive(false);

        _spriteBase.color = new(255, 255, 255, 0);
        _startColor = new(255, 255, 255, 128);
        _endColor = new(255, 255, 255, 255);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_builted) return;

        if (_playerIn && Input.GetKeyDown(KeyCode.E) && !_startBuilding && _playerItems.CanBuild(_woodCost))
            StartBuild();

        if (_startBuilding)
            Building();
    }

    private void StartBuild()
    {
        _playerItems.UseWood(_woodCost);
        _playerItems.transform.position = _playerBuildPosition.position;
        _playerAnimation.Building();
        _startBuilding = true;
        _spriteBase.color = _startColor;
        _houseCollider.SetActive(true);
    }

    private void Building()
    {
        _currentTimeToBuild += Time.deltaTime;

        if (_currentTimeToBuild >= _timeToBuild)
        {
            _playerAnimation.EndBuild();
            _spriteBase.color = _endColor;
            _builted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!collider.TryGetComponent(out _playerItems) || !collider.TryGetComponent(out _playerAnimation))
                throw new Exception("O script não está associado a este objeto");

            _playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _playerItems = null;
            _playerAnimation = null;
            _playerIn = false;
        }
    }
}
