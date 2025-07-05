using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateTankTurret : MonoBehaviour
{
    [SerializeField] Transform turretTransform;
    private bool isFacingRight = true;
    public static event Action<int,float> OnJoystickValueChanged;
    private Vector2 inputJoystickRotation;
    private float facingXValue;
    private float rotationAngle;



    private Quaternion initialTurretRotation;
    private Tank tank;

    private void Start()
    {
        initialTurretRotation = turretTransform.rotation;
        tank = GetComponent<Tank>();
    }

    private void FlipTurret()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void RotateRandomAngle()
    {
        float angle = UnityEngine.Random.Range(-45, 0f);
        Debug.Log(angle);
        Vector3 currentEuler = turretTransform.eulerAngles;
        turretTransform.rotation = Quaternion.Euler(angle,currentEuler.y, 0);
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (tank.tankSide != TankSideType.Player) return;

        inputJoystickRotation = context.ReadValue<Vector2>();
        
        if (inputJoystickRotation == Vector2.zero)
        {
            initialTurretRotation = turretTransform.rotation;
        }

        rotationAngle = inputJoystickRotation.x;

        if (isFacingRight)
        {
            facingXValue = inputJoystickRotation.x + inputJoystickRotation.y * -90;
            facingXValue = Mathf.Clamp(facingXValue, -45f, 45f);
            Quaternion targetRotation = Quaternion.Euler(facingXValue, 90, 0f);
            turretTransform.rotation = Quaternion.RotateTowards(turretTransform.rotation, targetRotation, Time.deltaTime * 100f);
        }

        if (!isFacingRight)
        {
            facingXValue = inputJoystickRotation.x + inputJoystickRotation.y * 90;
            facingXValue = Mathf.Clamp(facingXValue, -45f, 45f);
            Quaternion targetRotation = Quaternion.Euler(facingXValue, -90, 0f);
            turretTransform.rotation = Quaternion.RotateTowards(turretTransform.rotation, targetRotation, Time.deltaTime * 100f);
        }

        if (rotationAngle < 0 && isFacingRight)
        {
            FlipTurret();
        }

        if (rotationAngle > 0 && !isFacingRight) {
            FlipTurret();
        }
        
        if (facingXValue !=0)
        {
            float normalizedAngle = NormalizeAngle180(turretTransform.eulerAngles.x);
            OnJoystickValueChanged?.Invoke((int)normalizedAngle, (float)Math.Round(inputJoystickRotation.x, 2) * 100f);
        }

        float NormalizeAngle180(float angle)
        {
            angle %= 360f;
            if (angle > 180f)
            {
                angle -= 360f;
            }
            return -angle;
        }
    }
}
