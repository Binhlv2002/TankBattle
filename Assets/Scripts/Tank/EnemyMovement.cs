using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody rigidbodyTank;
    private Tank tank;
    private float movementInputValue;

    [SerializeField] float speed = 10f;
    [SerializeField] private float enemyDuration = 1f;
    [SerializeField] private float enemyMoveSpeed = 0.5f;
    private float enemyTimer = 0f;
    private bool isEnemyMoving = false;

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

    public async Task ExecuteMovement()
    {
        StartEnemyMovement();

        float timer = enemyDuration;
        while (timer > 0f && isEnemyMoving)
        {
            timer -= Time.deltaTime;
            await Task.Yield(); // Đợi một khung hình
        }

        StopEnemyMovement();
    }

    private void StartEnemyMovement()
    {
        isEnemyMoving = true;
        movementInputValue = Random.Range(-1f, 1f) * enemyMoveSpeed;
        tank.isMoving = true;
    }

    private void StopEnemyMovement()
    {
        isEnemyMoving = false;
        movementInputValue = 0f;
        tank.isMoving = false;
    }

    private void FixedUpdate()
    {
        // Enemy chỉ di chuyển khi là lượt enemy
        if (isEnemyMoving)
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

