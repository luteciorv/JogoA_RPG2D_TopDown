using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image _waterImage;
    [SerializeField] private Image _woodImage;
    [SerializeField] private Image _carrotImage;

    private PlayerItems _playerItems;

    private void Awake()
    {
        _playerItems = FindObjectOfType<PlayerItems>();
        if (_playerItems == null)
            throw new System.Exception("O objeto associado ao script não foi encontrado na cena");
    }

    // Start is called before the first frame update
    private void Start()
    {
        _waterImage.fillAmount = 0;
        _woodImage.fillAmount = 0;
        _carrotImage.fillAmount = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        _waterImage.fillAmount = _playerItems.CurrentWater / _playerItems.MaxWater;
        _woodImage.fillAmount = _playerItems.CurrentWood / _playerItems.MaxWood;
        _carrotImage.fillAmount = _playerItems.CurrentCarrot / _playerItems.MaxCarrot;

        Debug.Log(_woodImage.fillAmount);
    }
}
