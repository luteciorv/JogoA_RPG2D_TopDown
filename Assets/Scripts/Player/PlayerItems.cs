using System;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public float MaxWood { get; private set; }
    public int CurrentWood { get; private set; }

    public float MaxWater { get; private set; }
    public float CurrentWater { get; private set; }

    public float MaxCarrot { get; private set; }
    public int CurrentCarrot { get; private set; }

    public float MaxFishes { get; private set; }
    public int CurrentFishes { get; private set; }

    public bool CanWatering { get => CurrentWater > 0; }
    public bool CanCollectCarrot { get => CurrentCarrot < MaxCarrot; }
    public bool CanCollectFish { get => CurrentFishes < MaxFishes; }

    private void Start()
    {
        MaxWater = 50;
        MaxWood = 20;
        MaxCarrot = 20;
        MaxFishes = 10;
    }

    public void CollectWood()
    {
        if(CurrentWood < MaxWood)
            CurrentWood++;
    }

    public void CollectWater(float amount)
    {
        if (CurrentWater + amount < MaxWater)
            CurrentWater += amount;

        else
            CurrentWater = MaxWater;
    }

    public void UseWater()
    {
        if(CanWatering) CurrentWater -= 0.01f;
     
        if (CurrentWater < 0) CurrentWater = 0;
    }

    public void CollectCarrot()
    {
        CurrentCarrot++;
    }

    public void CollectFish()
    {
        CurrentFishes++;
    }

    public bool CanBuild(int woodCost) =>
        CurrentWood >= woodCost;

    public void UseWood(int wood) =>
        CurrentWood -= wood;
}
