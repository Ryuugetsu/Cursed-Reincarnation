using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    
   

}
