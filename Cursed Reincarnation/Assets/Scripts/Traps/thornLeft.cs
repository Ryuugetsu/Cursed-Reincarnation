using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thornLeft : MonoBehaviour
{
    public GameObject Trap;
    public bool Triggered = false;

    void OnTriggerEnter()
    {
        if (!Triggered)
        {
            Trap.GetComponent<Animation>().Play("Thorn");
            Triggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Triggered = false;
    }

}