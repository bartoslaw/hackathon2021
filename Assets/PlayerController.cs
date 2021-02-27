using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //X == JoystickButtoon0
    //A == JoystickButtonn1

    private KeyCode jumpKeyCode;
    private KeyCode dieKeyCode;
    private Rigidbody2D rb;
    private Animator animator;

    public float acc = 200.0f;
    public float speed = 15.0f;

    public float maxSpeed = 30.0f;

    public float jumpForce = 15.0f;

    private int health = 3;

    private Vector3 originalPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpKeyCode = KeyCode.Joystick1Button1;
        dieKeyCode = KeyCode.Joystick1Button0;
        originalPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (health == 0)
        {
            Destroy(this.gameObject);
        }

        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(jumpKeyCode))
        {
            rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);

            animator.SetBool("IsClacla", true);
        } else
        {
            animator.SetBool("IsClacla", false);
        }

        if (Input.GetKeyDown(dieKeyCode))
        {
            health -= 1;
        }

        if (horizontal > 0.0f)//going right
        {
            GetComponent<SpriteRenderer>().flipX = false;

            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
                rb.AddForce(new Vector2(speed, 0.0f));
        }
        else if (horizontal < 0.0f)//going left
        {
            GetComponent<SpriteRenderer>().flipX = true;

            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
                rb.AddForce(new Vector2(-speed, 0.0f));
        }

        if (rb.velocity.x == 0.0f && rb.velocity.y == 0)
        {
            //rb.AddForce(new Vector2(5.0f, 0.0f));
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bouncable")
        {
            rb.AddForce(collision.contacts[0].normal * 750.0f);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            float offset = 1.0f;

            if (transform.position.y > collision.gameObject.transform.position.y + offset)
            {
                Destroy(collision.gameObject);
                rb.AddForce(collision.contacts[0].normal * Random.Range(250.0f, 550.0f));
            } else if (transform.position.y < collision.gameObject.transform.position.y + offset / 2.0f)
            {
                health -= 1;

                Transform cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform;

                rb.velocity = Vector2.zero;
                transform.position = originalPosition + cameraPosition.position;
            }
        }
    }
}