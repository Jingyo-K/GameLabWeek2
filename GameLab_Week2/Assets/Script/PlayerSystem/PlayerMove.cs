using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float tiltAmount = 60f; // 기체가 기울어지는 정도
    public float rotationSpeed = 4f; // 기체 회전 속도
    Quaternion targetRotation;
    GameObject camObj;
    Camera cam;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camObj = transform.Find("Camera").gameObject;
        cam = transform.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        float moveSpeed = 10;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(horizontalInput * moveSpeed, verticalInput * moveSpeed, 0);

            // 기체 이동
        targetRotation = Quaternion.Euler(verticalInput * tiltAmount * 0.5f, -horizontalInput * tiltAmount, 0);

        transform.Find("Model").rotation = Quaternion.Lerp(transform.Find("Model").rotation, targetRotation, Time.deltaTime * rotationSpeed);
        camObj.transform.rotation = Quaternion.Euler(120, 0, 0);
    }
}
