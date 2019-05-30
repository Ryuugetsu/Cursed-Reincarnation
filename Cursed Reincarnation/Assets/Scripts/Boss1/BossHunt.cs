using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHunt : MonoBehaviour
{
    public Transform player;
    static Animator ani;
    public Slider healthbar;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthbar.value <= 0) return;

        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction,this.transform.forward);
        if (Vector3.Distance(player.position,this.transform.position) < 20 && angle < 30)
        {
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                      Quaternion.LookRotation(direction), 0.1f);
            ani.SetBool("isIdle", false);
            //ani.SetTrigger("roar");
            if(direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, 0.05f);
                ani.SetBool("isWalking", true);
                ani.SetBool("isAttacking", false);
            }
            else
            {
                ani.SetBool("isAttacking", true);
                ani.SetBool("isWalking", false);
            }
        }
        else
        {
            ani.SetBool("isIdle", true);
            ani.SetBool("isAttacking", false);
            ani.SetBool("isWalking", false);
        }
    }
}
