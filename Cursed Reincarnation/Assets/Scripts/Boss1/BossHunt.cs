using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossHunt : MonoBehaviour
{
    public Transform player;
    public GameObject Boss;
    public Slider healthbar;
    public Slider PlayerHealth;
    private Animator anim;
    private NavMeshAgent nav;
        
    public bool chasePlayer = true;
    private float StopDistance = 1.2f;
    private float distanceFromPlayer;

    public float delayAttack = 2.0f;
    private float attackCooldown;



    void Start ()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent <NavMeshAgent> ();
        nav.stoppingDistance = StopDistance;
        attackCooldown = Time.time;
    }

    void Update()
    {
        if (Boss.activeSelf) {
            ChaseTarget();
        }
    }

    void ChaseTarget()
    {
        distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        if(distanceFromPlayer >= StopDistance)
        {
            chasePlayer = true;
        }
        else
        {
            chasePlayer = false;
            Attack();
        }

        if (chasePlayer)
        {
            nav.SetDestination(player.position);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void Attack()
    {
       
        if (PlayerHealth.value > 0)
        {
            int randomAttack = Random.Range(1, 4);
            if (Time.time > attackCooldown)
            {
                Debug.Log("Attack!");
                anim.SetInteger("randomAttack", randomAttack);
                anim.SetTrigger("Attack");
                attackCooldown = Time.time + delayAttack;
            }
        }
        else
        {
            anim.SetTrigger("roar");
            return;
        }
    }





}