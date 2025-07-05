using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    private Rigidbody bulletRigibody;
    [SerializeField] private LayerMask tankMask;
    [SerializeField] private float maxDamage = 20f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float explosionForce = 1000f;

    private void Start()
    {
        bulletRigibody = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);

    }

    private void Update()
    {
        transform.forward = bulletRigibody.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);

        foreach (var collider in colliders)
        {
            Rigidbody tankRigibody = collider.GetComponent<Rigidbody>();
            if (!tankRigibody) continue;
            
            tankRigibody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            TankHealth tankHealth = tankRigibody.GetComponent<TankHealth>();
            if (!tankHealth) continue;
            float damage = CaculateDamage(tankRigibody.position);
            tankHealth.TakeDamage(damage);
           
        }
        Destroy(gameObject);
    }

    private float CaculateDamage(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        float relativeDistance = (explosionRadius - distance) / explosionRadius;
        float damage = relativeDistance * maxDamage;
        damage = Mathf.Max(0f, damage);
        return damage;
    }
}
