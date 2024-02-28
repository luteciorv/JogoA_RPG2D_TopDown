using System;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private float _speedToThrow;
    [SerializeField] private float _timeToMove;

    private float _timeCount;

    private void Update()
    {
        Throw();
    }

    private void Throw()
    {
        _timeCount += Time.deltaTime;

        if (_timeCount < _timeToMove)
            transform.Translate(_speedToThrow * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Collect(collider.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        if (!player.TryGetComponent(out PlayerItems playerItems))
            throw new Exception("O script não está associado ao objeto em questão");

        if (playerItems.CanCollectFish)
        {
            playerItems.CollectFish();
            Destroy(gameObject);
        }
    }
}
