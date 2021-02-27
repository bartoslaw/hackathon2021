using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //X == JoystickButtoon0
    //A == JoystickButtonn1

    private Text healthLabel;
    private Text pointsLabel;

    private KeyCode jumpKeyCode;
    private KeyCode dieKeyCode;
    private KeyCode respawnKeyCode;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource[] audioSources;


    public float acc = 200.0f;
    public float speed = 15.0f;

    public float maxSpeed = 30.0f;

    public float jumpForce = 15.0f;

    private int health = 100;
    private int points = 0;

    private bool amIAliveState = true;
    private Vector3 originalPosition;

    private Color originalColor;

    void Start()
    {
        pointsLabel = GameObject.FindGameObjectWithTag("PointsLabel").GetComponent<Text>();
        healthLabel = GameObject.FindGameObjectWithTag("HealthLabel").GetComponent<Text>();

        rb = GetComponent<Rigidbody2D>();
        jumpKeyCode = KeyCode.Joystick1Button1;
        dieKeyCode = KeyCode.Joystick1Button0;
        respawnKeyCode = KeyCode.Joystick1Button2;
        originalPosition = transform.position;
        animator = GetComponent<Animator>();
        originalColor = GetComponent<SpriteRenderer>().color;
        audioSources = GetComponents<AudioSource>();

        AddPoints();
        ChangeHealth();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    { 
        if (amIAliveState)
        {
            GetComponent<SpriteRenderer>().color = originalColor;
        } else
        {
            Color color = originalColor;
            color.a = 0.5f;

            GetComponent<SpriteRenderer>().color = color;
        }


        if (health <= 0)
        {
            SceneManager.LoadScene("background2");
            Destroy(this.gameObject);
        }

        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(jumpKeyCode))
        {
            rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            audioSources[1].Play();
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
        if (!amIAliveState)
        {
            return;
        }

        if (collision.gameObject.tag == "Lava")
        {
            audioSources[2].Play();
            Die();
        }

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
                rb.AddForce(collision.contacts[0].normal * Random.Range(450.0f, 650.0f));

                points += 500;
                audioSources[0].Play();
                    
                AddPoints();
            } else if (transform.position.y < collision.gameObject.transform.position.y + offset / 2.0f)
            {
                audioSources[3].Play();
                Die();
            }
        }
    }

    private void Die()
    {
        if (!amIAliveState)
        {
            return; 
        }

        health -= 1;

        Transform cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform;

        rb.velocity = Vector2.zero;
        transform.position = originalPosition + cameraPosition.position;

        ChangeHealth();
        amIAliveState = false;
        StartCoroutine(BringBack());
    }

    IEnumerator BringBack()
    {
        yield return new WaitForSeconds(1.5f);
        amIAliveState = true;
    }

    private void AddPoints()
    {
        pointsLabel.text = "Points: " + points;
    }

    private void ChangeHealth()
    {
        healthLabel.text = "Health: " + Mathf.Max(health, 0);
    }
}