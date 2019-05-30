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
    
        if(healthbar.value >= 0)
        {
            if(distance <= 20){
            // ... set the destination of the nav mesh agent to the player.
                anim.SetBool("isIdle",false);
                anim.SetBool("isWalking", true);
                nav.SetDestination (player.position);
            }
            else if(distance <= 5)
            {
                nav.Stop();
                anim.SetBool("isIdle",false);
                anim.SetBool("isWalking",false);
                anim.SetBool("isAttacking", true);
                nav.ResetPath();

            }
            else if(distance >= 30)
            {
                nav.Stop();
                anim.SetBool("isWalking",false);
                anim.SetBool("isAttacking",false);
                anim.SetBool("isIdle",true);
                nav.ResetPath();
            }
            
            //anim.SetBool("isAttacking", false); 
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
    }


}