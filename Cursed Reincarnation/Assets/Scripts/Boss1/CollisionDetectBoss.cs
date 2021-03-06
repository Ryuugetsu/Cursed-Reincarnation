﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetectBoss : MonoBehaviour
{   
    public Slider healthbar;
    Animator anim;
    public string opponent;
    public GameObject enemy;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != opponent) return;
        healthbar.value -= 50;
        
        if (healthbar.value <= 0)
        {
            anim.SetBool("isDead", true);
            Destroy(enemy,5.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
