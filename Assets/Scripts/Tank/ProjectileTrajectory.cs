using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrajectory : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefabs;
    [SerializeField] private Transform startPoint;
    [SerializeField] private TankShooting tankShooting;
    [SerializeField] private GameObject[] points;
    [SerializeField] private float firingForce;


    private void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = Instantiate(pointPrefabs, transform.position, Quaternion.identity);
        }

        startPoint = tankShooting.bulletTransform;
    }

    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position = SetupPointPosition(i * 0.15f);
        }
    }

    private Vector3 SetupPointPosition(float time)
    {
        firingForce = tankShooting.currentFiringForce;
        Vector3 direction = startPoint.forward;
        Vector3 gravityComponent = Physics.gravity * (time * time);
        Vector3 pointPostion = startPoint.position + (direction.normalized * firingForce * time) + (0.5f * gravityComponent);
        return pointPostion;
    }
}
