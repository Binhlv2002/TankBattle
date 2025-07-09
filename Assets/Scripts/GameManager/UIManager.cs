using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private OnScreenStick leftStick;
    [SerializeField] private OnScreenStick rightStick;
    [SerializeField] private TextMeshProUGUI angleText;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private Button fireButton;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    private void Awake()
    {
        GameManager.OnGameStateChanged += HandleOnGameStateChanged;
        RotateTankTurret.OnJoystickValueChanged += HandleOnJoystickValueChanged; ;
    }

    private void Update()
    {
        if (GameManager.instance.currentGameState == GameState.Victory)
        {
            victoryPanel.SetActive(true);
        }

        if (GameManager.instance.currentGameState == GameState.Defeat)
        {
            defeatPanel.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleOnGameStateChanged;
        RotateTankTurret.OnJoystickValueChanged -= HandleOnJoystickValueChanged;
    }
    private void HandleOnJoystickValueChanged(int angle, float power)
    {
        angleText.text = angle.ToString();
        powerText.text = power.ToString();
    }

    private void HandleOnGameStateChanged(GameState gameState)
    {
        fireButton.interactable = gameState == GameState.PlayerTurn;
        leftStick.enabled = gameState == GameState.PlayerTurn;
        rightStick.enabled = gameState == GameState.PlayerTurn;
    }

    public void OnFireButtonPress()
    {
        GameManager.instance.UpdateGameState(GameState.EnemyTurn);
    }
}