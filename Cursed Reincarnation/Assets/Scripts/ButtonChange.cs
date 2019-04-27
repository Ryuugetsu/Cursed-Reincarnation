﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonChange : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject optionPanel;
   


    public Color original;
    public Color activeColor;
   

    private void Update()
    {
        if (optionPanel.activeInHierarchy)
        {
            text.color = activeColor;
        }
        else
        {
            text.color = original;
        }
    }
}
