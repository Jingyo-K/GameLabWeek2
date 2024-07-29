using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * Time.deltaTime *5);
        if(transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }
}
