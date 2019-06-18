using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openChest : MonoBehaviour
{
    public GameObject Chest;
    public bool Triggered = false;

    void OnTriggerEnter()
    {
        if (!Triggered)
        {
            Chest.GetComponent<Animation>().Play("Open");
            Triggered = true;
        }
        else
        {
            Chest.GetComponent<Animation>().Play("close");
            Triggered = false;
        }
    }

    /*
    void OnTriggerExit(Collider other)
    {
        Triggered = false;
    }
    */
}