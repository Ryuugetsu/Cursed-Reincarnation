using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    public Camera baseCam;


    void Awake()
    {
        baseCam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + baseCam.transform.rotation * Vector3.back,
                          baseCam.transform.rotation * Vector3.down);
    }
}
