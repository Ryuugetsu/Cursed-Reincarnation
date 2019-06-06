using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossHunt : MonoBehaviour
{
    public Transform player;               // Reference to the player's position.
    public GameObject rightFist;
    public GameObject leftFist;
    public Slider healthbar;        // Reference to this enemy's health.
    static Animator anim;
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    bool isDead = false;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        anim = GetComponent<Animator>();
        nav = GetComponent <NavMeshAgent> ();
        nav.updateRotation = true;
        nav.updatePosition = true;
    }

    public void enableFist()
    {
        rightFist.GetComponent<Collider>().enabled = true;
        leftFist.GetComponent<Collider>().enabled = true;
    }

    public void disableFist()
    {
        rightFist.GetComponent<Collider>().enabled = false;
        leftFist.GetComponent<Collider>().enabled = false;
    }


    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        int randomAttack = Random.Range(1, 4);
        if (healthbar.value >= 0 && !isDead)
        {
            if (distance < 20 && distance > 1.2)
            {
                nav.isStopped = false;
                nav.updatePosition = true;
                nav.SetDestination(player.position);
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", true);

            }
            else if (distance <= 1.2)
            {
                nav.velocity = Vector3.zero;
                nav.isStopped = true;
                anim.SetTrigger("Attack");
                anim.SetInteger("randomAttack", randomAttack);


            }
            else
            {
                //nav.updatePosition = false;
                nav.isStopped = false;
                anim.SetBool("isIdle", true);

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