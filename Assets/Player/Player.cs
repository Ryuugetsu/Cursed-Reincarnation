﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool isGrounded;
    public bool isCrouching;

    private float speed;
    private float w_speed = 0.05f;
    private float r_speed = 0.1f;
    private float c_speed = 0.025f;
    private float rotSpeed = 3.0f;
    private float jumpHeight = 135.0f;

    Rigidbody rb;
    Animator anim;
    CapsuleCollider col;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        isGrounded = true;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(isCrouching)
            {
                isCrouching = false;
                anim.SetBool("isCrouching", false);
                col.height = 2;
                col.center = new Vector3(0,1,0);
            }
            
            else
            {
                isCrouching = true;
                anim.SetBool("isCrouching", true);
                speed = c_speed;
                col.height = 1;
                col.center = new Vector3(0, 0.5f, 0);
            }
        }

        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal") * rotSpeed;
        
        transform.Translate(0,0,z);
        transform.Rotate(0,y,0);

        if(Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(0, jumpHeight, 0);
            anim.SetTrigger("isJumping");
            isCrouching = false;
            isGrounded = false;
        }

        if(isCrouching)
        {
            if(Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true); 
            }
        }

        else if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = r_speed;
            //Run
            if(Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true); 
            }
        }

        else if(!isCrouching)
        {
            speed = w_speed;
            //Standing
            if(Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true); 
            }
        }

	}

    void OnCollisionEnter()
    {
        isGrounded = true;
    }


}

	