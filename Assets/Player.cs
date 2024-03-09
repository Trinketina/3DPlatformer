using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
        [SerializeField] float speed = 1;
    float verticalMovement;
    float horizontalMovement;
    float forwardMovement;
    [SerializeField] float jumpStrength = 1;

    [SerializeField] PlayerStats stats;

    [SerializeField] Animator anim;
    bool onGround = false;

    Rigidbody rb;
    Transform cam;

    int immunityFrames;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;

        stats.Score = 0;
        stats.Health = 20;
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Raycast(transform.position, Vector3.down, .9f);
        anim.SetBool("IsGrounded", onGround);

        Debug.DrawLine(transform.position, //start position
            transform.position + Vector3.down * .9f, //end position
            Color.red);

        forwardMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && onGround)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
        
        //danace
        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetInteger("DanceVal", Random.Range(0, 10));
            anim.SetTrigger("Dance");   
        }

        //tests for out of bounds
        if (transform.position.y < -5 && stats.Health > 0)
        {
            Hurt(stats.MaxHealth);
        }
    }
    private void FixedUpdate()
    {
        if (stats.Health > 0)
        {
            if (immunityFrames > 0)
                immunityFrames--;
            if (immunityFrames < 5)
            {
                Vector3 camForward = cam.forward;
                Vector3 camRight = cam.right;
                camForward.y = 0;
                camRight.y = 0;
                camForward.Normalize();
                camRight.Normalize();

                Vector3 movementVector = (camForward * forwardMovement + camRight * horizontalMovement).normalized * speed;

                anim.SetFloat("Speed", movementVector.magnitude);
                if (movementVector != Vector3.zero)
                    anim.transform.forward = movementVector;
                movementVector.y = rb.velocity.y;
                if (onGround)
                    rb.velocity = movementVector;
            }
        }
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void AddToScore(int s)
    {
        stats.Score += s;
        if (stats.Score == 5)
            Win();
    }

    public void Hurt(int h)
    {
        if (immunityFrames <= 0)
        {
            stats.Health -= h;
            immunityFrames = 10;
        }
        if (stats.Health <= 0)
            Die();
    }

    public void Heal(int h)
    {
        stats.Health += h;
    }

    void Die()
    {
        //make the camera zoom in on miku and lock
        //play miku death animation
        Time.timeScale = 0f;
        SceneManager.LoadScene("Game Over", LoadSceneMode.Additive);
    }

    void Win()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("Game Win", LoadSceneMode.Additive);
    }
}
