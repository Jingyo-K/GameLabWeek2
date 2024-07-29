using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    public float Speed = 10;
    public float damage = 10;
    public GameObject explosion;
    void FixedUpdate()
    {
        
        transform.position += transform.up * Time.deltaTime * Speed;
        Speed -=  Time.deltaTime*10;
        if(Speed <= 0)
        {
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            exp.GetComponent<EnemyBlast>().damage = damage;
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
