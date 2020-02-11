using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public int damage = 50;

    public float explosionRadius = 0f;
    public GameObject impactEffect;

    private EnemyHealth _enemyHealth;

    [SerializeField] private Rigidbody _rigidbody;

    private void Start() { _rigidbody.AddForce(transform.forward * MainTurretController.ProjectileThrust); }

//    void Update()
//    {
//        if (target == null)
//        {
//            Destroy(gameObject);
//            return;
//        }
//
//        Vector3 dir = target.position - transform.position;
//        float distanceThisFrame = speed * Time.deltaTime;
//
//        if (dir.magnitude <= distanceThisFrame)
//        {
//            HitTarget();
//            return;
//        }
//
//        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
//        transform.LookAt(target);
//    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Tags.ENEMY_TAG))
            HitTarget(other);
    }

    void HitTarget(Collision other)
    {
        _enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth>();
        _enemyHealth.ReduceHealth(MainTurretController.ProjectileDamage, false);

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}