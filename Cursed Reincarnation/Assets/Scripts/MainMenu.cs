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
        } //Verifica se existe algum save no game se ouver ativa os botões continuar e carregar jogo              

        mainMenu.SetActive(false);
        titleScreen.SetActive(true);

    }
    void Update()
    {
        TitleScreen();

        if(menuConfig.activeInHierarchy == true && mainMenu.activeInHierarchy == false && Input.GetKeyDown(KeyCode.Escape))
        {
            menuConfig.SetActive(false);
            mainMenu.SetActive(true);
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

            GameManager.gameManager.hasSelected = false;
            mainMenu.SetActive(true);
            titleScreen.SetActive(false);
            menuConfig.SetActive(false);
        }
    }

    public void LoadConfig()
    {

        //load configurações gráficas
        menuConfigComp.fullscreenSelector.index = PlayerPrefs.GetInt("Fullscreen", 0);
        menuConfigComp.monitorSelector.index = PlayerPrefs.GetInt("Monitor", 0);
        menuConfigComp.GetResolutions();
        menuConfigComp.resolutionsDropdown.value= PlayerPrefs.GetInt("Resolution", 0);
        menuConfigComp.qualitySelector.index = PlayerPrefs.GetInt("Quality", 0);
        menuConfigComp.vSyncSelector.index = PlayerPrefs.GetInt("V-Sync", 0);
        menuConfigComp.antiAliasingSelector.index = PlayerPrefs.GetInt("Anti-Aliasing", 0);
        menuConfigComp.motionBlurSelector.index = PlayerPrefs.GetInt("MotionBlur", 0);
        menuConfigComp.ambientOclusionSelector.index = PlayerPrefs.GetInt("AmbientOclusion", 0);
        menuConfigComp.bloomSelector.index = PlayerPrefs.GetInt("Bloom", 0);
        menuConfigComp.depthOfFieldSelector.index = PlayerPrefs.GetInt("DepthOfField", 0);
        menuConfigComp.ChromaticSelector.index = PlayerPrefs.GetInt("ChromaticAberration", 0);

        //load configurações de som
        menuConfigComp.sliderVolumePrincipal.value = PlayerPrefs.GetFloat("VolumePrincipal", 0);
        menuConfigComp.sliderVolumeSFX.value = PlayerPrefs.GetFloat("VolumeSFX", 0);
        menuConfigComp.sliderVolumeMusica.value = PlayerPrefs.GetFloat("VolumeMusica", 0);

        Debug.Log("Configurações Careegadas!");

    }
}
