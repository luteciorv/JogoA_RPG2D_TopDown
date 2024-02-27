using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [SerializeField] private int _maxWoods;
    private int _currentWoods;

    [SerializeField] private float _maxWater;
    private float _currentWater;

    public bool CanWatering { get => _currentWater > 0; }

    public void CollectWood()
    {
        if(_currentWoods < _maxWoods)
            _currentWoods++;
    }

    public void CollectWater(float amount)
    {
        if (_currentWater + amount < _maxWater)
            _currentWater += amount;

        else
            _currentWater = _maxWater;
    }

    public void UseWater()
    {
        if(CanWatering) _currentWater -= 0.01f;
     
        if (_currentWater < 0) _currentWater = 0;
    }
}
