using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber = 1;
    public PlayerInput playerInput;

    [SerializeField] float speed = 10f;

    private Rigidbody rigidbodyTank;
    private Tank tank;

    private float movementInputValue;

    

    private void Awake()
    {
        rigidbodyTank = GetComponent<Rigidbody>();
        tank = GetComponent<Tank>();

    }

    private void OnEnable()
    {
        rigidbodyTank.isKinematic = false;
        movementInputValue = 0f;
    }

    private void OnDisable()
    {
        rigidbodyTank.isKinematic = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Chỉ di chuyển khi là lượt player
        if (GameManager.instance.currentGameState != GameState.PlayerTurn) return;

        if (!tank.haveFuelLeft)
        {
            movementInputValue = 0f;
            tank.isMoving = false;
            return;
        }
        if (context.performed || context.canceled)
        {

            Vector2 inputMovement = context.ReadValue<Vector2>();
            movementInputValue = inputMovement.x;
            tank.isMoving = movementInputValue == 0f ? false : true;
        }
    }
    private void FixedUpdate()
    {
        // Player chỉ di chuyển khi là lượt player
        if (GameManager.instance.currentGameState == GameState.PlayerTurn)
        {
            MoveTank();
        }
    }

    private void MoveTank()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
        rigidbodyTank.MovePosition(rigidbodyTank.position + movement);
    }
}
