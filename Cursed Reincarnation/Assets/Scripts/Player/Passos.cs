using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Passos : MonoBehaviour
{
    private AudioSource steps;

    private void Start()
    {
        steps = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.tag == "Ground")
            {
                steps.Play();
            }
        }
    }
}
