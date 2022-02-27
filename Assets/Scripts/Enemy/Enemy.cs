using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private string currentState = "IdleState";
    private Transform target;
    public float chaseRange = 5;
    public Animator animator;
    public float speed = 3;
    public float attackRange = 2;

    public int health = 100;
    public int maxHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.gameOver)
        {
            animator.enabled = false;
            this.enabled = false;
        }
        float distance = Vector3.Distance(transform.position, target.position);

        if(currentState == "IdleState")
        {
            if (distance < chaseRange)
                currentState = "ChaseState";
        }
        else if(currentState == "ChaseState")
        {
            animator.SetTrigger("chase");
            animator.SetBool("isAttacking", false);

            if(distance < attackRange)
            {
                currentState = "AttackState";
            }

            if(target.position.x > transform.position.x)
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }else
            {
                transform.Translate(-transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (currentState == "AttackState")
        {
            animator.SetBool("isAttacking", true);
            if (distance > attackRange)
            {
                currentState = "ChaseState";
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        currentState = "ChaseState";

        if(health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log("enemy is dead");
        animator.SetTrigger("isDead");

        GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;
    }
}
