using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public bool hasSave;

    [SerializeField] private GameObject continuar = null;
    [SerializeField] private GameObject carregar = null;

    [SerializeField] private GameObject menuConfig = null;
    [SerializeField] private GameObject titleScreen = null;
    [SerializeField] private GameObject mainMenu = null;

    private MenuConfig menuConfigComp;
   

    // Start is called before the first frame update
    void Start()
    {
        menuConfigComp = menuConfig.GetComponent<MenuConfig>();

        //LoadConfig();

        {
            if (hasSave)
            {
                continuar.SetActive(true);
                carregar.SetActive(true);
            }
            else
            {
                continuar.SetActive(false);
                carregar.SetActive(false);
            }            
        } //Verifica se existe algum save no game se ouver ativa os botões continuar e carregar jogo              

        mainMenu.SetActive(false);
        titleScreen.SetActive(true);

    }
    void Update()
    {
        TitleScreen();

        /*
        if(menuConfig.activeInHierarchy == true && mainMenu.activeInHierarchy == false && Input.GetKeyDown(KeyCode.Escape))
        {
            menuConfig.SetActive(false);
            mainMenu.SetActive(true);
        }*/
    }

    public void NewGame()
    {
        //envia para a proxima scene de acordo com a ordem na lista de build
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      
    }

    public void QuitGame()
    {
        //fecha o jogo
        Application.Quit();
    }

    public void Continuar() { }

    public void Carregar() { }

    private void TitleScreen()
    {       
        if (Input.anyKey && titleScreen.activeInHierarchy)
        {

            GameManager.gameManager.hasSelected = false;
            mainMenu.SetActive(true);
            titleScreen.SetActive(false);
            menuConfig.SetActive(false);
        }
    }

    
}
