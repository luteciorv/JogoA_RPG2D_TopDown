using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Tools")]
    [SerializeField] private Image _axeImage;
    [SerializeField] private Image _shovelImage;
    [SerializeField] private Image _bucketImage;

    [Header("Collectables")]
    [SerializeField] private Image _waterImage;
    [SerializeField] private Image _woodImage;
    [SerializeField] private Image _carrotImage;

    private Color32 _selectedTool;
    private Color32 _unselectedTool;

    private PlayerItems _playerItems;
    private Player _player;

    private void Awake()
    {
        _playerItems = FindObjectOfType<PlayerItems>();
        if (_playerItems == null)
            throw new Exception("O objeto associado ao script não foi encontrado na cena");

        _player = FindObjectOfType<Player>();
        if (_player == null)
            throw new Exception("O objeto associado ao script não foi encontrado na cena");
    }

    // Start is called before the first frame update
    private void Start()
    {
        _waterImage.fillAmount = 0;
        _woodImage.fillAmount = 0;
        _carrotImage.fillAmount = 0;

        _selectedTool = new(255, 255, 255, 255);
        _unselectedTool = new(255, 255, 255, 128);

        _axeImage.color = _unselectedTool;
        _shovelImage.color = _unselectedTool;
        _bucketImage.color = _unselectedTool;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateCollectableHUD();
        UpdateToolsHUD();
    }

    private void UpdateCollectableHUD()
    {
        _waterImage.fillAmount = _playerItems.CurrentWater / _playerItems.MaxWater;
        _woodImage.fillAmount = _playerItems.CurrentWood / _playerItems.MaxWood;
        _carrotImage.fillAmount = _playerItems.CurrentCarrot / _playerItems.MaxCarrot;
    }

    private void UpdateToolsHUD()
    {
        _axeImage.color = _unselectedTool;
        _shovelImage.color = _unselectedTool;
        _bucketImage.color = _unselectedTool;

        switch(_player.CurrentTool)
        {
            case 1:
                _axeImage.color = _selectedTool;
                break;

            case 2:
                _shovelImage.color = _selectedTool;
                break;

            case 3:
                _bucketImage.color = _selectedTool;
                break;

            default:
                throw new Exception($"Nenhuma ferramenta de numeração {_player.CurrentTool} existe para ser selecionada");
        }
    }
}
