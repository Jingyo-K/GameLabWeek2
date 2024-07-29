using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    Weapons.WeaponType weaponType;
    public enum MoveType
    {
        Straight,
        Spread,
        Homing
    }
    public MoveType moveType;

    //*********호밍 변수**********

    GameObject[] targets;
    GameObject target;
    private bool isHoming;
    public float randY;
    public float randX;
    Vector3 direction;
    private bool once = false;
    Rigidbody rbm;
    
    //*****************************
    public enum AttackType
    {
        Common,
        Penetrate,
        Blast
    }
    public AttackType attackType;
    //*********관통 변수***********
    public int penetrateCount;
    //*****************************

    //*********폭발 변수***********
    public GameObject explosionEffect;
    public float blastRadius;
    //*****************************


    private float damage;
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    
    void Start()
    {
        isHoming = false;
        randY = Random.Range(-1f, -0.1f);
        randX = Random.Range(-1f, 1f);
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        if(targets.Length != 0)
        {
            target = targets[Random.Range(0, targets.Length)];
        }
        else
        {
            target = null;
        }
        rbm = GetComponent<Rigidbody>();
        weaponType = _PlayerManager.Instance.GetEquipment(Equipment.EquipmentType.Weapon).GetComponent<Weapons>().GetWeaponDamageType();
    }

    void FixedUpdate()
    {
        Move();
        if (transform.position.y > 16 || transform.position.y < -16 || transform.position.x > 19 || transform.position.x < -19)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    {
        if(moveType == MoveType.Straight || moveType == MoveType.Spread)
            transform.Translate(Vector3.up * Time.deltaTime * 30);
        else if(moveType == MoveType.Homing)
        {
            homingStart();
        }
    }

    private void homingStart()
    {
        if (isHoming)
        {
            if (target != null)
            {
            direction = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.FromToRotation(transform.up, direction) * transform.rotation;
            transform.rotation = rotation;
            }
            transform.Translate(transform.up * Time.deltaTime * 20);
        }
        else
        {
            rbm.velocity = new Vector3(randX, randY, 0).normalized * 10;
            if (!once)
                StartCoroutine(Homing(target));
        }
    }

    private IEnumerator Homing(GameObject target) 
    {
        once = true;
        yield return new WaitForSeconds(0.3f);
        isHoming = true;
        rbm.velocity = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            if(attackType == AttackType.Common)
            {
                other.gameObject.GetComponent<EnemyHPSystem>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if(attackType == AttackType.Penetrate)
            {
                other.gameObject.GetComponent<EnemyHPSystem>().TakeDamage(damage);
                if(penetrateCount > 0)
                {
                    penetrateCount--;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if(attackType == AttackType.Blast)
            {
                GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                effect.transform.localScale = new Vector3(blastRadius, blastRadius, blastRadius);
                effect.transform.GetChild(0).GetComponent<Blast>().SetDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
