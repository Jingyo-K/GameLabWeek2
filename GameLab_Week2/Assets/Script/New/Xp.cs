using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xp : MonoBehaviour
{
    private bool targeting = false;
    public GameObject target;
    void Start()
    {
        StartCoroutine(StartTargeting());
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(targeting)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            transform.position += targetDir.normalized * 20* Time.deltaTime;
        }
    }

    IEnumerator StartTargeting()
    {
        yield return new WaitForSeconds(0.5f);
        targeting = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            PlayerLVSystem.instance.playerEXP += 1;
            Destroy(gameObject);
        }
    }

}
