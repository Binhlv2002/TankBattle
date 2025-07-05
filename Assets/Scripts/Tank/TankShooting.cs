using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public static TankShooting instance;
    public int playerNumber  = 1;
    [SerializeField] private Rigidbody bulletRigibody;
    [SerializeField] public Transform bulletTransform;
    [SerializeField] private float minFiringForce = 15f;
    [SerializeField] private float maxFiringForce = 30f;
    public float currentFiringForce { get; private set; }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        RotateTankTurret.OnJoystickValueChanged += HandleTankPowerForce;
    }

    private void HandleTankPowerForce(int angle, float power)
    {
        currentFiringForce = Mathf.Max(minFiringForce, maxFiringForce * power / 100);
    }

    private void OnEnable()
    {
        currentFiringForce = minFiringForce;
    }

    public void Fire()
    {
        Rigidbody bulletInstance = Instantiate(bulletRigibody, bulletTransform.position, bulletTransform.rotation);
        bulletInstance.velocity = bulletTransform.forward * currentFiringForce;
        Debug.Log("Firing Force: " + currentFiringForce);

    }

    public void SetupRequimentStatForAI()
    {
        currentFiringForce = GenerateRandomPowerForce();
        //Debug.Log(currentFiringForce);
    }

    private float GenerateRandomPowerForce()
    {
        return Random.Range(minFiringForce, maxFiringForce);
    }
    private void OnDestroy()
    {
        RotateTankTurret.OnJoystickValueChanged -= HandleTankPowerForce;
    }

}
