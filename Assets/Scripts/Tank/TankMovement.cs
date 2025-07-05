using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    public int playerNumber = 1;
    public PlayerInput playerInput;
    private Rigidbody rigidbodyTank;
    private Tank tank;
    private float movementInputValue;
    private float turnInputValue;

    [SerializeField] float speed = 10f;
    [SerializeField] float turnSpeed = 100f;

    [SerializeField] private float enemyDuration = 1.5f;
    [SerializeField] private float enemySpeed = 5f;
    private float enemyTimer = 0f;


    private void Awake()
    {
        rigidbodyTank = GetComponent<Rigidbody>();
        tank = GetComponent<Tank>();
    }

    private void OnEnable()
    {
        rigidbodyTank.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;

        if (tank.tankSide == TankSideType.Enemy)
        {
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        }
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        if (state == GameState.EnemyTurn && tank.tankSide == TankSideType.Enemy)
        {
            enemyTimer = enemyDuration;
            enemySpeed = Random.Range(-1f, 1f);

        }
    }

    private void OnDisable()
    {
        rigidbodyTank.isKinematic = true;
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (tank.tankSide == TankSideType.Enemy) return;

        if (!tank.haveFuelLeft)
        {
            movementInputValue = 0f;
            tank.isMoving = false;
            return;
        }

        Vector2 inputMovement = context.ReadValue<Vector2>();
        movementInputValue = inputMovement.x;
        tank.isMoving = movementInputValue == 0f ? false : true;
        
    }

    private void Update()
    {
      
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        MoveTank();
    }

    private void MoveTank()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
        rigidbodyTank.MovePosition(rigidbodyTank.position + movement);
    }
}

