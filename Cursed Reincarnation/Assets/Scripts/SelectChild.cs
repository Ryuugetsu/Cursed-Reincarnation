using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectChild : MonoBehaviour
{
    private GameObject child;

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManager.hasSelected == false )
        {
            EventSystem.current.SetSelectedGameObject(null);

            //Seleciona o primeiro filho deste gameObject
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                child = this.gameObject.transform.GetChild(0).gameObject;
                EventSystem.current.SetSelectedGameObject(child);
            }

            //Seleciona o ultimo filho deste gameObject
            if (Input.GetKeyDown(KeyCode.UpArrow) )
            {
                child = this.gameObject.transform.GetChild(transform.childCount - 1).gameObject;
                EventSystem.current.SetSelectedGameObject(child);

            }
        }
    }
   
}
