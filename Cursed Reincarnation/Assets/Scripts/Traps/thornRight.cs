using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thornRight : MonoBehaviour
{
    public GameObject Trap;
    public bool Triggered = false;

    void OnTriggerEnter()
    {
        if (!Triggered)
        {
            Trap.GetComponent<Animation>().Play("ThornRight");
            Triggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Triggered = false;
    }

}