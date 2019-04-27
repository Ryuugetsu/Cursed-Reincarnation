using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuConfig : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI graficsCardText;

    public AudioMixer audioMixer;
    public Slider sliderVolumePrincipal;
    public Slider sliderVolumeSFX;
    public Slider sliderVolumeMusica;
    
    public HorizontalSelector fullscreenSelector;


    [SerializeField] private GameObject[] optionsPanels;
     private int indexPanel = 0;


    // Start is called before the first frame update
    void Start()
    {
        //chama o metodo de pegar o nome da placa de video da maquina em que o jogo esta sendo executado
        GraficCard();     
    }


    // Update is called once per frame
    void Update()
    {

        FullScreen(fullscreenSelector.index); //envia o indice da posição do vetor que contem as opções de fullscreen
        ChangeOptionsPanelsByButton();
    }

    //troca os painels usando os botões Q E
    private void ChangeOptionsPanelsByButton()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            indexPanel++;
            if (indexPanel > optionsPanels.Length-1)
            {
                indexPanel = 0;
            }
            ChangeOptionsPanels(optionsPanels[indexPanel]);
            GameManager.gameManager.hasSelected = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            indexPanel--;
            if (indexPanel < 0)
            {
                indexPanel = optionsPanels.Length-1;
            }
            ChangeOptionsPanels(optionsPanels[indexPanel]);
            GameManager.gameManager.hasSelected = false;


        }
    }

    //desativas todos os paineis de opções e ativa o selecionado efetuado a troca entre os paineis de graficos, jogabilidade, som, etc..
    public void ChangeOptionsPanels(GameObject CurrentPanel)
    {
        foreach (GameObject panels in optionsPanels)
        {
            panels.SetActive(false);
        }
        CurrentPanel.SetActive(true);
    }

    //configurações de graficos
    public void FullScreen(int state)
    {
        bool fullscreen = true;
        if (state == 0)
        {
            fullscreen = true;
        }
        else if(state == 1)
        {
            fullscreen = false;
        }


        //full screen
        Screen.fullScreen = fullscreen;
        fullscreenSelector.index = state;
           
    }


    //pega o nome e o tamanho da memoria da placa de video e seta na variavel de texto nomeada graficsCardText
    private void GraficCard()
    {
        if(SystemInfo.graphicsDeviceName != null)
        {
            graficsCardText.text = SystemInfo.graphicsDeviceName + " - " + SystemInfo.graphicsMemorySize + " MB";
        }
        else
        {
            graficsCardText.text = "";
        }
    }


    //configurações de som
    public void SetVolumePrincipal(float volume)
    {
        audioMixer.SetFloat("VolumePrincipal", volume);
    }
    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("VolumeSFX", volume);
    }
    public void SetVolumeMusica(float volume)
    {
        audioMixer.SetFloat("VolumeMusica", volume);
    }


    //save playerprefs
    private void OnDisable()
    {
        float volumePrincipal = 0;
        float volumeSFX = 0;
        float volumeMusica = 0;

        //graficos
        PlayerPrefs.SetInt("Fullscreen", fullscreenSelector.index);

        //Som
        audioMixer.GetFloat("VolumePrincipal", out volumePrincipal);    //pega o Exposed Parameter nomeado "VolumePrincipal" do audioMixer e manda para a variavel local de nome volumePrincipal 
        audioMixer.GetFloat("VolumeSFX", out volumeSFX);
        audioMixer.GetFloat("VolumeMusica", out volumeMusica);

        PlayerPrefs.SetFloat("VolumePrincipal", volumePrincipal);   //Cria um chave de nome "VolumePrincipal" e salva o valor contido na variavel volumePrincipal nela
        PlayerPrefs.SetFloat("VolumeSFX", volumeSFX);
        PlayerPrefs.SetFloat("VolumeMusica", volumeMusica);


        PlayerPrefs.Save();
        Debug.Log("Configurações salvas!");

    }

}
