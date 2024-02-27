using System;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private int _waterValue;

    private bool _playerIn;
    private PlayerItems _playerItems;

    // Update is called once per frame
    private void Update()
    {
        Collect();
    }

    private void Collect()
    {
        if(_playerIn && Input.GetKeyDown(KeyCode.E))
            _playerItems.CollectWater(_waterValue);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!collider.TryGetComponent(out _playerItems))
                throw new Exception("O script não está associado a este objeto");

            _playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _playerItems = null;
            _playerIn = false;
        }
    }
}
