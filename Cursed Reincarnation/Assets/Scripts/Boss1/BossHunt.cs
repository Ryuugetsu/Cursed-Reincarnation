using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossHunt : MonoBehaviour
{
    public Transform player;               // Reference to the player's position.
    public Slider healthbar;        // Reference to this enemy's health.
    static Animator anim;
    NavMeshAgent nav;               // Reference to the nav mesh agent.
    bool isDead = false;


    void Awake ()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        anim = GetComponent<Animator>();
        nav = GetComponent <NavMeshAgent> ();
    }


    void Update ()
    {
        float distance = Vector3.Distance(player.position, transform.position);
    
        if(healthbar.value >= 0 && !isDead)
        {
            if(distance < 20 && distance > 2){
                nav.updatePosition = true;
                nav.SetDestination (player.position);
                anim.SetBool("isIdle",false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);
                
            }
            else if(distance <= 2)
            {
                nav.updatePosition = false;
                anim.SetBool("isIdle",false);
                anim.SetBool("isWalking",false);
                anim.SetBool("isAttacking", true);
            }
            else{
                anim.SetBool("isIdle",false);
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