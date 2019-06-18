using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCollisionDetect : MonoBehaviour
{
    public Slider healthbar;
    Animator anim;
    public string opponent;
    public GameObject enemy;
    public GameObject boss;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != opponent) return;
        anim.SetTrigger("hit");
        healthbar.value -= 25;
        if (healthbar.value <= 0)
        {
            anim.SetBool("isDead", true);
            //tela de game over ~~
        }
    }


}