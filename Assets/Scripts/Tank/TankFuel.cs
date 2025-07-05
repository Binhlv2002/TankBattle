using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankFuel : MonoBehaviour
{
    [SerializeField] private Tank playerTank;
    [SerializeField] private Slider fuelProgress;
    [SerializeField] private int currentFuel;
    [SerializeField] private int maxFuel = 1000;
    [SerializeField] private int fuelConsumption;

    void Start()
    {
        fuelProgress.GetComponent<Slider>();
        currentFuel = maxFuel;
        fuelProgress.value = currentFuel;
    }

    private void Update()
    {
        ConsumeFuelWhileMoving();
    }

    private void ConsumeFuelWhileMoving()
    {
        if (playerTank.isMoving)
        {
            currentFuel -= fuelConsumption;
            fuelProgress.value = currentFuel;
            if (currentFuel <= 0)
            {
                playerTank.haveFuelLeft = false;
            }
        }
    }

    public void RefillFuelAfterTurn()
    {
        currentFuel = maxFuel;
        fuelProgress.value = currentFuel;
    }

    public void SetFuelCondition()
    {
        playerTank.haveFuelLeft = true;
    }
}
