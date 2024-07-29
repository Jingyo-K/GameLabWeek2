using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public enum BulletType
    {
        Normal,
        Homing
    }
    public BulletType bulletType;
    public float Speed = 10;
    public float damage = 10;
    void FixedUpdate()
    {
        if(bulletType == BulletType.Normal)
        {
            transform.position += transform.up * Time.deltaTime * Speed;
        }

        if(transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }
    public void SetBulletDirection(Vector3 direction)
    {
        transform.up = direction;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHPSystem>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
