using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Vector3 TargetPos;

    public Transform groundCheck;
    public CharacterController controller;
    public Animator animator;

    public float groundCheckDistance = 0.4f;
    public float gravityScale = -9.8f;
    public float jumpForce = 2f;

    float speed;
    public float speedSimple;
    public float speedShift;

    float xAxes;
    float zAxes;

    Vector3 velocity;

    bool isGrounded;
    bool isJump;

    public LayerMask groundLayer;

 
    void Start()
    {
        speed = speedSimple;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayer);

        if (isGrounded) isJump = true;
        Controll();
    }

    void Controll()
    {
        xAxes = Input.GetAxis("Horizontal");
        zAxes = Input.GetAxis("Vertical");

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("Jump", false);
        }

        Vector3 movement = transform.right * xAxes + transform.forward * zAxes;
        if (Mathf.Abs(xAxes) + Mathf.Abs(zAxes) > 0)
        {
            animator.SetBool("isRanning", true);
        }
        else
        {
            animator.SetBool("isRanning", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speedShift;
        }
        else
        {
            speed = speedSimple;
        }

        controller.Move(movement * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isJump)
        {
            animator.SetBool("Jump", true);
            if (!isGrounded) isJump = false;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravityScale);
        }

        velocity.y += gravityScale * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.transform.position, groundCheckDistance);
    }
}
