using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHunt : MonoBehaviour
{
    public Transform player;               // Reference to the player's position.
    public Slider healthbar;        // Reference to this enemy's health.
    static Animator anim;
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = true;
        nav.updatePosition = true;
    }


    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (healthbar.value >= 0 && !isDead)
        {
            if (distance < 20 && distance > 1)
            {
                nav.isStopped = false;
                nav.updatePosition = true;
                nav.SetDestination(player.position);
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);

            }
            else if (distance <= 1)
            {
                nav.velocity = Vector3.zero;
                nav.isStopped = true;
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);


            }
            else
            {
                //nav.updatePosition = false;
                nav.isStopped = false;
                anim.SetBool("isIdle", true);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", false);

            }


            //anim.SetBool("isAttacking", false); 
        }

        else
        {
            nav.updatePosition = false;
            isDead = true;
            //anim.SetBool("isDead", true);
            nav.enabled = false;
        }
    }





}