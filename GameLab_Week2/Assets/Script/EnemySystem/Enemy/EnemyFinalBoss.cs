using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFinalBoss : EnemyPattern
{
    public GameObject[] bulletPrefab; // 0: 원형 탄환(느림), 1: 직선 탄환(빠름), 2: 타원형 탄환
    public GameObject Player;
    private bool isPattern = false;
    private int randomPattern;
    private int randX;
    private int randY;
    
    void Start()
    {
        Player = GameObject.Find("Player");
    }
    void Update()
    {
        if (!isPattern)
        {
            randomPattern = Random.Range(0, 4);
            switch (randomPattern)
            {
                case 0:
                    StartCoroutine(pattern1());
                    break;
                case 1:
                    StartCoroutine(pattern2());
                    break;
                case 2:
                    StartCoroutine(pattern3());
                    break;
                case 3:
                    StartCoroutine(pattern4());
                    break;
                case 4:
                    StartCoroutine(pattern5());
                    break;
            }   
        }
        transform.Rotate(0,0.3f,0);
    }
    private IEnumerator pattern1()
    {
        isPattern = true;
        Vector3 currentPos = transform.position;
        Vector3 targetPos = new Vector3(-7f,13f,0f);
        float movetime = 1;
        float elapsedTime = 0;
        while (elapsedTime < movetime)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, (elapsedTime / movetime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        transform.GetChild(1).gameObject.SetActive(true);
        currentPos = transform.position;
        targetPos = new Vector3(5f,13f,0f);
        elapsedTime = 0;
        while (elapsedTime < movetime)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, (elapsedTime / movetime));
            elapsedTime += Time.deltaTime * 0.5f;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        transform.GetChild(1).gameObject.SetActive(false);
        currentPos = transform.position;
        targetPos = new Vector3(0f,12f,0f);
        elapsedTime = 0;
        while (elapsedTime < movetime)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, (elapsedTime / movetime));
            elapsedTime += Time.deltaTime*0.5f;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        isPattern = false;
    }

    private IEnumerator pattern2()
    {
        isPattern = true;
        yield return new WaitForSeconds(1.0f);
        float rightshootangle = 90;
        float leftshootangle = -90;
        for (int i = 0; i < 30; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab[0], transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetBulletDirection(Quaternion.Euler(0, 0, rightshootangle) * -Vector3.up);
            bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            bullet = Instantiate(bulletPrefab[0], transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetBulletDirection(Quaternion.Euler(0, 0, leftshootangle) * -Vector3.up);
            bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            yield return new WaitForSeconds(0.1f);
            rightshootangle -= 3;
            leftshootangle += 3;
            if(rightshootangle < 7.5f)
            {
                rightshootangle = 7.5f;
            }
            if(leftshootangle > -7.5f)
            {
                leftshootangle = -7.5f;
            }
        }
        for(int i = 0; i<30; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab[0], transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetBulletDirection(Quaternion.Euler(0, 0, rightshootangle) * -Vector3.up);
            bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            bullet = Instantiate(bulletPrefab[0], transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetBulletDirection(Quaternion.Euler(0, 0, leftshootangle) * -Vector3.up);
            bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            yield return new WaitForSeconds(0.1f);
            rightshootangle += 1;
            leftshootangle += 1;
        }
        for(int i = 0; i<60; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab[0], transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetBulletDirection(Quaternion.Euler(0, 0, rightshootangle) * -Vector3.up);
            bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            bullet = Instantiate(bulletPrefab[0], transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetBulletDirection(Quaternion.Euler(0, 0, leftshootangle) * -Vector3.up);
            bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            yield return new WaitForSeconds(0.1f);
            rightshootangle -= 1;
            leftshootangle -= 1;
        }
        yield return new WaitForSeconds(1.0f);
        isPattern = false;
    }

    private IEnumerator pattern3()
    {
        isPattern = true;
        Vector3 currentPos;
        float movetime = 1;
        float elapsedTime = 0;

        for(int i =0; i < 4; i++)
        {
            currentPos = transform.position;
            Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), Random.Range(7f, 13f), 0f);
            movetime = 1;
            elapsedTime = 0;
            while (elapsedTime < 0.25f * movetime)
            {
                transform.position = Vector3.Lerp(currentPos, randomPos, (elapsedTime / movetime));
                elapsedTime += Time.deltaTime*0.5f;
                yield return null;
            }
            GameObject bullet = Instantiate(bulletPrefab[1], transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBomb>().SetBulletDirection((Player.transform.position - transform.position).normalized);
            bullet.GetComponent<EnemyBomb>().damage = GetComponent<EnemyStatus>().Attack;
            bullet.GetComponent<EnemyBomb>().Speed = Random.Range(10,20);
            while (elapsedTime < 0.5f * movetime)
            {
                transform.position = Vector3.Lerp(currentPos, randomPos, (elapsedTime / movetime));
                elapsedTime += Time.deltaTime*0.5f;
                yield return null;
            }
            GameObject bullet1 = Instantiate(bulletPrefab[1], transform.position, Quaternion.identity);
            bullet1.GetComponent<EnemyBomb>().SetBulletDirection((Player.transform.position - transform.position).normalized);
            bullet1.GetComponent<EnemyBomb>().damage = GetComponent<EnemyStatus>().Attack;
            bullet1.GetComponent<EnemyBomb>().Speed = Random.Range(10,20);
            while (elapsedTime < 0.75f * movetime)
            {
                transform.position = Vector3.Lerp(currentPos, randomPos, (elapsedTime / movetime));
                elapsedTime += Time.deltaTime*0.5f;
                yield return null;
            }
            GameObject bullet2 = Instantiate(bulletPrefab[1], transform.position, Quaternion.identity);
            bullet2.GetComponent<EnemyBomb>().SetBulletDirection((Player.transform.position - transform.position).normalized);
            bullet2.GetComponent<EnemyBomb>().damage = GetComponent<EnemyStatus>().Attack;
            bullet2.GetComponent<EnemyBomb>().Speed = Random.Range(10,20);
            while (elapsedTime < movetime)
            {
                transform.position = Vector3.Lerp(currentPos, randomPos, (elapsedTime / movetime));
                elapsedTime += Time.deltaTime*0.5f;
                yield return null;
            }
            GameObject bullet3 = Instantiate(bulletPrefab[1], transform.position, Quaternion.identity);
            bullet3.GetComponent<EnemyBomb>().SetBulletDirection((Player.transform.position - transform.position).normalized);
            bullet3.GetComponent<EnemyBomb>().damage = GetComponent<EnemyStatus>().Attack;
            bullet3.GetComponent<EnemyBomb>().Speed = Random.Range(10,20);
        }

        currentPos = transform.position;
        Vector3 targetPos = new Vector3(0f,12f,0f);
        elapsedTime = 0;
        while (elapsedTime < movetime)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, (elapsedTime / movetime));
            elapsedTime += Time.deltaTime*0.5f;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        isPattern = false;
    }

    private IEnumerator pattern4()
    {
        isPattern = true;
        yield return new WaitForSeconds(1.0f);
        GameObject bullet;
        int count = 0;
        float spreadAngle = 60.0f;
        int SpreadLine = 5; // 탄환을 방사형으로 발사할 개수
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        
        while (count < 5)
        {
            for (int i = 0; i < SpreadLine; i++)
            {
                // 각 탄환의 각도를 계산
                float angle = spreadAngle * ((float)i / (SpreadLine - 1) - 0.5f);
                Vector3 rotatedDirection = Quaternion.Euler(0, 0, angle) * direction;
                
                bullet = Instantiate(bulletPrefab[2], transform.position, Quaternion.identity);
                bullet.GetComponent<EnemyBullet>().SetBulletDirection(rotatedDirection);
                bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            }
            yield return new WaitForSeconds(0.1f);
            count++;
        }
        isPattern = false;
    }

    private IEnumerator pattern5()
    {
        isPattern = true;
        yield return new WaitForSeconds(1.0f);
        isPattern = false;
    }
}
