using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falseFloor : MonoBehaviour
{
    public GameObject TrapDoor;
    public bool Triggered = false;

    void OnTriggerEnter()
    {
        if (!Triggered)
        {
            TrapDoor.GetComponent<Animation>().Play("FloorAnim");
            Triggered = true;
        }
    }
}
