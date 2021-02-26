using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //X == JoystickButtoon0
    //A == JoystickButtonn1

    private KeyCode jumpKeyCode;
    private Rigidbody2D rb;

    public float acc = 200.0f;
    public float speed = 15.0f;

    public float maxSpeed = 30.0f;

    public float jumpForce = 15.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpKeyCode = KeyCode.Joystick1Button1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(jumpKeyCode))
        {
            //yVelocity += jumpForce;
            rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
        }

        if (horizontal > 0.0f)//going right
        {
            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
                rb.AddForce(new Vector2(speed, 0.0f));
        }
        else if (horizontal < 0.0f)//going left
        {
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
    }
}