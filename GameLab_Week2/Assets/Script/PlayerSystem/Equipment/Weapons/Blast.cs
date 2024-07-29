using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    Material material;
    public float damage;

    void Start()
    {
        material = transform.GetComponent<MeshRenderer>().material;
        material.SetFloat("_BlastRadius", 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - Time.deltaTime*2);
        if (material.color.a <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHPSystem>().TakeDamage(damage);
        }
    }
}
