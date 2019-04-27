using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public bool hasSave;

    [SerializeField] private GameObject continuar;
    [SerializeField] private GameObject carregar;

    [SerializeField] private GameObject menuConfig;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject mainMenu;

   

    // Start is called before the first frame update
    void Start()
    {
        LoadConfig();

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
            menuConfig.SetActive(true);
            menuConfig.SetActive(false);
        } //Verifica se existe algum save no game se ouver ativa os botões continuar e carregar jogo              

        mainMenu.SetActive(false);
        menuConfig.SetActive(false);
        titleScreen.SetActive(true);

    }
    void Update()
    {
        TitleScreen();

        if(menuConfig.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(true);
            menuConfig.SetActive(false);
        }
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
            mainMenu.SetActive(true);
            titleScreen.SetActive(false);
        }
    }

    public void LoadConfig()
    {
        menuConfig.SetActive(true);

        //load configurações gráficas
        menuConfig.GetComponent<MenuConfig>().fullscreenSelector.index = PlayerPrefs.GetInt("Fullscreen", 0);

        //load configurações de som
        menuConfig.GetComponent<MenuConfig>().sliderVolumePrincipal.value = PlayerPrefs.GetFloat("VolumePrincipal", 0);
        menuConfig.GetComponent<MenuConfig>().sliderVolumeSFX.value = PlayerPrefs.GetFloat("VolumeSFX", 0);
        menuConfig.GetComponent<MenuConfig>().sliderVolumeMusica.value = PlayerPrefs.GetFloat("VolumeMusica", 0);

        menuConfig.SetActive(false);
    }
}
