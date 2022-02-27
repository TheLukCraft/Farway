using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed = 8;

    public float jumpForce = 10;
    public float gravity = -20;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool ableToMakeADoubleJump = true;

    public Animator animator;
    public Transform model;

    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;

        animator.SetFloat("speed", Mathf.Abs(hInput));

        bool isGrounder = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isGrounder);
        if(isGrounder)
        {
            direction.y = -1;
            ableToMakeADoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                animator.SetTrigger("fireBallAttack");
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
            if (ableToMakeADoubleJump & Input.GetButtonDown("Jump"))
            {
                DoubleJump();
            }
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("FireBall Attack"))
        {
            return;
        }
        if(hInput != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(hInput, 0, 0));
            model.rotation = newRotation;
        }
        void DoubleJump()
        {
            animator.SetTrigger("doubleJump");
            direction.y = jumpForce;
            ableToMakeADoubleJump = false;
        }
        void Jump()
        {
            direction.y = jumpForce;
        }
       
        controller.Move(direction * Time.deltaTime);
    }
}
