using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private string gameDataFileName = "PortuguesMenu.json";

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
    
    private void LoadText()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            //GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
    }

   

    public void HardDeselect()
    {
        hasSelected = false;
    }
}
