using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1 : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float frequency = 20f;

    [SerializeField]
    float magnitute = 0.5f;

    bool facingRight = false;

    Vector3 pos, localScale;

    void Start()
    {
        moveSpeed = Random.Range(5.0f, 9.0f);
        GetComponent<SpriteRenderer>().flipX = true;
        pos = transform.position;
        localScale = transform.localScale;

        Collider2D[] childrenColliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in childrenColliders)
        {
            if (col != GetComponent<Collider2D>())
            {
                print("Ignoring");
                Physics2D.IgnoreCollision(col, GetComponent<Collider2D>());
            }
        }
    }

    void Update()
    {
        //CheckWhereToFace();

        if (facingRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    void MoveRight()
    {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitute;
    }

    void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitute;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (facingRight)
        {
            facingRight = false;
            GetComponent<SpriteRenderer>().flipX = true;
        } else
        {
            facingRight = true;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}
