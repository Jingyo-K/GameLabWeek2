using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlast : MonoBehaviour
{
    Material material;
    public float damage;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void FixedUpdate()
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - Time.deltaTime);
        if (material.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHPSystem>().TakeDamage(damage);
        }
    }
}
