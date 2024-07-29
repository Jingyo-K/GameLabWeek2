using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : Equipment
{
    public enum WeaponType
    {
        Laser,
        Blast,
        Railgun
    }
    public WeaponType weaponType;
    public GameObject BulletPrefab;
    public enum WeaponRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    public WeaponRarity weaponRarity;
    public float DamageCoefficient;
    public float FireRateCoefficient;    
    public int bulletCount;
    private bool cooltime = false;
    private float spreadAngle = 0;

    //Apperance
    public Vector3 scale;
    public Color color;

    public WeaponType GetWeaponDamageType()
    {
        return weaponType;
    }

    void Start()
    {
        
    }

    void OnEnable()
    {
        cooltime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!cooltime)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        cooltime = true;
        _PlayerManager.Status status = _PlayerManager.Instance.GetCurrentStatus();
        GameObject bullet = null;
        switch (BulletPrefab.GetComponent<Bullet>().moveType)
        {
            case Bullet.MoveType.Spread:
                spreadAngle = 3 *bulletCount;
                float angleStep = spreadAngle / (bulletCount - 1);
                float angle = -spreadAngle / 2; // 부채꼴의 시작 각도
                    if(bulletCount == 1)
                    {
                        bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                        bullet.GetComponent<Bullet>().SetDamage(status.Damage);
                    }
                    else
                    {
                        for (int i = 0; i < bulletCount; i++)
                        {
                            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                            rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                            bullet = Instantiate(BulletPrefab, transform.position, rotation);
                            bullet.GetComponent<Bullet>().SetDamage(status.Damage);
                            angle += angleStep;
                        }
                    }
                break;
            case Bullet.MoveType.Homing:
                for(int i = 0; i < bulletCount; i++)
                {
                    bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().SetDamage(status.Damage);
                }
                break;
            case Bullet.MoveType.Straight:
                float SpreadLength = 0.5f;
                float spreadOffset = (-SpreadLength / 2)*(bulletCount-1);
                Vector3 spawnPosition = new Vector3(transform.position.x + spreadOffset, transform.position.y, transform.position.z);

                for(int i = 0; i < bulletCount; i++)
                {
                    bullet = Instantiate(BulletPrefab, spawnPosition, Quaternion.identity);
                    bullet.GetComponent<Bullet>().SetDamage(status.Damage);
                    spawnPosition.x += SpreadLength;
                }
                break;
        }
        yield return new WaitForSeconds(status.FireRate);
        cooltime = false;
    }
}
