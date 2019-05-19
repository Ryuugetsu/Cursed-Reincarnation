using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    [SerializeField] private Rigidbody gateTriggerRight; //objeto de interalçao no cenario para abrir o portão
    [SerializeField] private Rigidbody gateTriggerLeft;
    
    [SerializeField] private bool keyRight = false; //variavel que armazena se o trigger foi ou n ativado
    [SerializeField] private bool keyLeft = false;


    [SerializeField] private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag != null)
        {
            if (Input.GetButtonDown("Submit"))
            {
                
            }
        }
    }
}
