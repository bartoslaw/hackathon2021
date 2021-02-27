using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float magnitute = 0.5f;

    Vector3 pos;


    float x;
    float y;
    float z;



    void Start()
    {
        pos = transform.position;
        x = pos.x;
        y = Random.Range(-5, 4);
        z = pos.z;
        pos = new Vector3(x, y, z);
        transform.position = pos;

    }

    void Update()
    {
        MoveLeft();
    }


    void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * magnitute;
    }



}
