using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerProjectile : MonoBehaviour
{

    [SerializeField] protected float speed = 15;

    Transform targetTransform;

    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }

    private void Update()
    {
        transform.LookAt(targetTransform);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hitEnemy = collision.other.GetComponent<BasicEnemy>();
        if (hitEnemy != null)
        {
            var collisions = Physics.OverlapSphere(transform.position, 15);
            for (int i = 0; i < collisions.Length; i++)
            {
                var enemy = collisions[i].GetComponent<BasicEnemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(100);
                }
            }
        }
    }
}
