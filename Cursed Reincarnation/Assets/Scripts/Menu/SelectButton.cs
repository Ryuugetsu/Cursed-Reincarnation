using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private GameObject firstButton = null;
    [SerializeField] private GameObject optionalFirstButton = null;
    [SerializeField] private GameObject lastButton = null;


    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager.hasSelected == false)
        {
            EventSystem.current.SetSelectedGameObject(null);

            //Seleciona o primeiro filho deste gameObject
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (firstButton.activeInHierarchy)
                {
                    EventSystem.current.SetSelectedGameObject(firstButton);
                }
                else
                {
                    EventSystem.current.SetSelectedGameObject(optionalFirstButton);
                }
                GameManager.gameManager.hasSelected = true;
            }

            //Seleciona o ultimo filho deste gameObject
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                EventSystem.current.SetSelectedGameObject(lastButton);
                GameManager.gameManager.hasSelected = true;

            }
        }
    }

    
    public void OnSelect(BaseEventData eventData)
    {
        GameManager.gameManager.hasSelected = true;

    }
}
