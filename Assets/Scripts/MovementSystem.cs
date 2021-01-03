﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController characterController;
    public float speed;
    public float turnSmothTime = 0.1f;
    public float turnSmothVelocity;
    private Transform camera;
    private Animator animator;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float speedMovementAnimstion;

    private Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGround;

    public bool isGiftInHnad = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main.transform;
        animator.SetFloat("SpeedMovement", speedMovementAnimstion);
        animator.SetBool("IsGiftInHand", isGiftInHnad);
    }

  
    void Update()
    {

        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("Jump", false);
        }

        if (Input.GetButtonDown("Jump") && isGround)
        {
            animator.SetBool("Jump", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Game.isPause) return;


        float movementHorizontal = Input.GetAxis("Horizontal");
        float movementVertical = Input.GetAxis("Vertical");


        if (Mathf.Abs(movementHorizontal) + Mathf.Abs(movementVertical) > 0) animator.SetBool("isRanning", true);
        else animator.SetBool("isRanning", false);


        velocity.y += gravity * Time.deltaTime;

        Vector3 direction = new Vector3(movementHorizontal, 0, movementVertical).normalized;
        //Vector3 direction = new Vector3(movementHorizontal, 0, movementVertical);

        Vector3 moveDirection = Vector3.zero;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmothVelocity, turnSmothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //print(movementHorizontal + "   " + moveDirection.x);

        }

        Vector3 move = moveDirection * speed;
        move.y = velocity.y;

        characterController.Move(move * Time.deltaTime);

        /* if (Input.GetKeyDown(KeyCode.U)) SetAnimatiomLooting();
         if (Input.GetKeyDown(KeyCode.I)) SetAnimationBlowing();
         if (Input.GetKeyDown(KeyCode.O)) SetAnimationKissing();
         */
    }

   


    public void SetAnimatiomLooting()
    {
        animator.SetTrigger("Loting");
    }

    public void SetAnimationKissing()
    {
        animator.SetTrigger("Kissing");
    }

    public void SetAnimationBlowing()
    {
        animator.SetBool("isRanning", false);
        animator.SetTrigger("Blowing");
    }

    public void SetGiftinHand()
    {
        isGiftInHnad = true;
        animator.SetBool("IsGiftInHand", isGiftInHnad);
    }
}
