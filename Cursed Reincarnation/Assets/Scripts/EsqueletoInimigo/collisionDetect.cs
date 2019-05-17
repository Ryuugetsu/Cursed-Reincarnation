using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collisionDetect : MonoBehaviour
{   
    public Slider healthbar;
    Animator anim;
    public string opponent;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != opponent) return;
        healthbar.value -= 25;
        if(healthbar.value <= 0)
            anim.SetBool("isDead",true);
    
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
