using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyPattern1 : EnemyPattern
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
            randomPattern = Random.Range(0, 2);
            switch (randomPattern)
            {
                case 0:
                    StartCoroutine(pattern1());
                    break;
                case 1:
                    StartCoroutine(pattern2());
                    break;
            }
            isPattern = true;
            
        }
    }

    IEnumerator pattern1() // 탄환 산개
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
                
                bullet = Instantiate(bulletPrefab[0], transform.position, Quaternion.identity);
                bullet.GetComponent<EnemyBullet>().SetBulletDirection(rotatedDirection);
                bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            }
            yield return new WaitForSeconds(0.1f);
            count++;
        }
        yield return new WaitForSeconds(1.0f);
        isPattern = false;
    }

    IEnumerator pattern2() // 탄환 집중
    {
        isPattern = true;
        yield return new WaitForSeconds(1.0f);
        GameObject bullet;
        int count = 0;
        Vector3 SpawnPosition = transform.position;
        Vector3 CurrentPosition = transform.position;
        Vector3 position = transform.position;
        float elapsedTime;
        float moveDuration;
        while (count < 5)
        {   
            CurrentPosition = transform.position;
            randX = Random.Range(-7, 8);
            randY = Random.Range(7, 13);
            position.x = randX;
            position.y = randY;
            SpawnPosition = transform.position;
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            bullet = Instantiate(bulletPrefab[1], SpawnPosition, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetBulletDirection(direction);
            bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            yield return new WaitForSeconds(0.1f);
            SpawnPosition.x += -0.5f;
            bullet = Instantiate(bulletPrefab[1], SpawnPosition, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetBulletDirection(direction);
            bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            yield return new WaitForSeconds(0.1f);
            SpawnPosition.x += 0.5f;
            bullet = Instantiate(bulletPrefab[1], SpawnPosition, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetBulletDirection(direction);
            bullet.GetComponent<EnemyBullet>().damage = GetComponent<EnemyStatus>().Attack;
            count++;
            elapsedTime = 0f;
            moveDuration = 0.4f;
            while (elapsedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(CurrentPosition, position, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = position;
            yield return new WaitForSeconds(0.5f);
            
        }
        elapsedTime = 0f;
        moveDuration = 2.0f;
        CurrentPosition = transform.position;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(CurrentPosition, new Vector3(0,12,0), elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isPattern = false;
    }
}
