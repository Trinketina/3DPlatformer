using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        anim.SetBool("IsGrounded",onGround);

        forwardMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
        Debug.DrawLine(transform.position, //start position
            transform.position + Vector3.down * .9f, //end position
            Color.red);
    }
    private void FixedUpdate()
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
        rb.velocity = movementVector;
    }


    public void AddToScore(int s)
    {
        stats.Score += s;
    }

    public void Hurt(int h)
    {
        stats.Health -= h;
    }

    public void Heal(int h)
    {
        stats.Health += h;
    }
}
