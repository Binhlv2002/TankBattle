using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentGameState;
    public Tank playerTank;
    public Tank enemyTank;
    public TankFuel playerTankFuel;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateGameState(GameState gameState)
    {
        currentGameState = gameState;

        switch (gameState)
        {
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.EnemyTurn:
                HandleEnemyTurn();
                break;
            case GameState.Decision:
                HandleDecision();
                break;
            case GameState.Victory:
                Debug.Log("Game State: Game Win");
                break;
            case GameState.Defeat:
                Debug.Log("Game State: Game Over");
                break;
        }

        OnGameStateChanged?.Invoke(gameState);

    }

    private async void HandleDecision()
    {
        await Task.Delay(1000);

        if (!enemyTank.gameObject.activeSelf)
        {
            UpdateGameState(GameState.Victory);
        }
        else if (!playerTank.gameObject.activeSelf)
        {
            UpdateGameState(GameState.Defeat);
        }
        else
        {
            UpdateGameState(GameState.PlayerTurn);
        }
    }

    private async void HandleEnemyTurn()
    {
        await Task.Delay(3000);
        if (enemyTank.gameObject.activeSelf)
        {
            enemyTank.GetComponent<TankShooting>().SetupRequimentStatForAI();
            enemyTank.GetComponent<RotateTankTurret>().RotateRandomAngle();
            await Task.Delay(500);
            enemyTank.GetComponent<TankShooting>().Fire();
            await Task.Delay(2000);
        }
        UpdateGameState(GameState.Decision);
    }

    private void HandlePlayerTurn()
    {
        playerTankFuel.RefillFuelAfterTurn();
        playerTankFuel.SetFuelCondition();
    }

}