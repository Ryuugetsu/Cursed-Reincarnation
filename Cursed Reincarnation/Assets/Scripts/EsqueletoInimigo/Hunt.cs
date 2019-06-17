﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Hunt : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    public Slider healthbar;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthbar.value <= 0) return;
        
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(player.position, this.transform.position) < 10 && angle < 60)
        {

            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                Quaternion.LookRotation(direction), 0.1f);

            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            if (direction.magnitude > 1)
            {
                this.transform.Translate(0,0,0.05f);
                anim.SetBool("isWalking",true);
                anim.SetBool("isAttacking",false);
            }
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }
        }
        else
        {
            anim.SetBool("isIdle",true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }
}
