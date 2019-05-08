using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;


    public bool hasSelected = false;

    private void Awake()
    {
        //testa se existem outros Game Manager na scene, se ouver, os destroi
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); //evita que o Game Manager seja destruido ao trocar de fases para n perder os dados atuais não salvos
    }
    public void Update()
    {
        if (Input.GetAxis("Mouse X") != 0.0f || Input.GetAxis("Mouse Y") != 0.0f)
        {
            hasSelected = false;
        }
    }
    
   

   

    public void HardDeselect()
    {
        hasSelected = false;
    }
}
