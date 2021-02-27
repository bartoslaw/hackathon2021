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

    AudioSource audioSource;

    void Start()
    {
        moveSpeed = Random.Range(5.0f, 9.0f);
        GetComponent<SpriteRenderer>().flipX = true;
        print(GetComponent<AudioSource>());
        audioSource = GetComponent<AudioSource>();

        transform.position = GameObject.FindGameObjectWithTag("Spawner").transform.position;
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
        if (collision.gameObject.tag == "Lava")
        {
            Destroy(this.gameObject);
        }

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
