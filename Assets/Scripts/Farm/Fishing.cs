using System;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private GameObject _fish;
    [SerializeField] private float _chanceToCatchFish;

    private bool _playerIn;
    private PlayerItems _playerItems;
    private PlayerAnimation _playerAnimation;

    // Update is called once per frame
    private void Update()
    {
        ToFish();
    }

    private void ToFish()
    {
        if (_playerIn && Input.GetKeyDown(KeyCode.E))
        {
            _playerAnimation.Fishing();
        }
    }

    public void CatchFish()
    {
        var currentChanceToCatchFish = UnityEngine.Random.Range(0, 101);
        if (currentChanceToCatchFish <= _chanceToCatchFish)
        {
            var newPosition = _playerItems.gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-0.7f, -1f), UnityEngine.Random.Range(0, 0.5f), 0f);
            Instantiate(_fish, newPosition, transform.rotation);
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
