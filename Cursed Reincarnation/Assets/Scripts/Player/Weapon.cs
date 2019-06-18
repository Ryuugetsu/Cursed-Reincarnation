using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //player script

    [SerializeField]
    public GameObject item1;
    public bool showItem;


    void Start()
    {
        showItem = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (showItem == false)
        {
            item1.SetActive(false);
        }
        if (showItem == true)
        {
            item1.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && showItem == false)
        {
            showItem = true;
            //gameObject.GetComponent<Player>().isEquiped = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            showItem = false;
            //gameObject.GetComponent<Player>().isEquiped = false;
        }

    }
}
