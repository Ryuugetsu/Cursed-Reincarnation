using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class MenuConfig : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI graficsCardText = null;

    public AudioMixer audioMixer;
    public Slider sliderVolumePrincipal;
    public Slider sliderVolumeSFX;
    public Slider sliderVolumeMusica;

    public PostProcessProfile mainPostProcessProfile;
    private PostProcessLayer mainPostProcessLayer;

    public HorizontalSelector fullscreenSelector;
    private bool fullscreen = true;
    public HorizontalSelector monitorSelector;
    int m_monitorIndex = -1;
    private Resolution[] resolutions;
    public HorizontalSelector qualitySelector;
    public TMP_Dropdown resolutionsDropdown;
    public HorizontalSelector vSyncSelector;
    public HorizontalSelector antiAliasingSelector;
    public HorizontalSelector motionBlurSelector;
    public HorizontalSelector ambientOclusionSelector;
    public HorizontalSelector bloomSelector;
    public HorizontalSelector depthOfFieldSelector;
    public HorizontalSelector ChromaticSelector;


    [SerializeField] private GameObject[] optionsPanels = null;
    private int indexPanel = 0;

    //Esse if é um treta sininstra que vc concerteza n vai querer entender como funciona, só oq importa saber é que ele cria o metodo SetPosition(posX, posY), que serve para setar a posição da janela do jogo durante o Runtime
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(System.String className, System.String windowName);

    public static void SetPosition(int x, int y, int resX = 0, int resY = 0)
    {
        SetWindowPos(FindWindow(null, "Cursed Reincarnation"), 0, x, y, resX, resY, resX * resY == 0 ? 1 : 0);
    }
#endif


    // Start is called before the first frame update
    void Start()
    {
        mainPostProcessLayer = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessLayer>();

        GetResolutions();

        GraficCard(); //chama o metodo de pegar o nome da placa de video da maquina em que o jogo esta sendo executado


        //verifica quantos monitores estão conectados e alimenta o MonitorHorizontalSelector
        Debug.Log("displays connected: " + Display.displays.Length);
        monitorSelector.data.Clear();
        monitorSelector.data.Add("Monitor " + 1);
        if (Display.displays.Length == 2)
        {
            monitorSelector.data.Add("Monitor " + 2);
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        SetFullScreen(fullscreenSelector.index); //envia o indice da posição do vetor que contem as opções de fullscreen
        SetQuality(qualitySelector.index);
        SetVSync(vSyncSelector.index);
        SetAntiAliasing(antiAliasingSelector.index);
        SetMotionBlur(motionBlurSelector.index);
        SetAmbientOclusion(ambientOclusionSelector.index);
        SetBloom(bloomSelector.index);
        SetDephOfField(depthOfFieldSelector.index);
        SetChromatic(ChromaticSelector.index);

        ChangeOptionsPanelsByButton();

        SetMonitor(monitorSelector.index);


    }



    //configurações de graficos
    public void SetFullScreen(int fullscreenIndex) //Fullscreen 
    {
        if (fullscreenIndex == 0)
        {
            fullscreen = true;
        }
        else if (fullscreenIndex == 1)
        {
            fullscreen = false;
        }
        
        //full screen
        Screen.fullScreen = fullscreen;
        fullscreenSelector.index = fullscreenIndex;
    }
    
    public void SetMonitor(int monitorIndex) //Monitor
    {
        if (monitorIndex != m_monitorIndex)
        {

            if (monitorIndex == 0)
            {
                SetPosition(0, 0);
                StartCoroutine(OnMonitorSwith());
                m_monitorIndex = monitorIndex;

            }
            else if(monitorIndex == 1)
            {
                if (Display.displays.Length == 2)
                {
                    SetPosition(-Display.displays[1].systemWidth, 0);

                    StartCoroutine(OnMonitorSwith());
                    m_monitorIndex = monitorIndex;
                }
                else if(Display.displays.Length == 1)
                {
                    monitorSelector.index = 0;
                    //SetPosition(0, 0);
                    monitorSelector.OnLeftClicked();
                }

            }

        }
    }

    IEnumerator OnMonitorSwith()
    {
        if (Screen.fullScreen == false)
        {
            Screen.fullScreen = true;
            yield return new WaitForSeconds(0.1f);
            Screen.fullScreen = false;
            GetResolutions();
            resolutionsDropdown.value = resolutionsDropdown.value - 1;            

        }
        else
        {
            GetResolutions();
        }
    }

    public void GetResolutions() //Metodo que alimenta o resolutionsDropdown com as resoluções compativeis com seu monitor. 
    {

        resolutions = Screen.resolutions; //1- Alimenta o Vector resoltion com todas as resoluções compativeis com o monitor.

        resolutionsDropdown.ClearOptions(); //2- Apaga o conteudo atual do dropdown.

        List<string> options = new List<string>(); //3- Cria uma lista de strings que vai em breve armazenar as resoluções convertidas em string.

        int currentResolutinIndex = 0; //8- Variavel que irá armazenar o indice da lista que correponde a resolução usada atualmente.
        for (int i = 0; i < resolutions.Length; i++) //4- Laço que percorre todas as resoluções.
        {
            string option = resolutions[i].width + " x " + resolutions[i].height; //5- Converte as resoluções em string.

            options.Add(option); //6- Alimenta a lista com a resolução convertida em string.
            
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)//9- Verifica se a resolução atual é a mesma do tamanho do seu monitor.
            {
                currentResolutinIndex = i; //10- Armazena o indice da lista correnpondente a resolução atual
            }
        }
        resolutionsDropdown.AddOptions(options); //7- Alimenta o dropdown com os valores da lista de strings.
        resolutionsDropdown.value = currentResolutinIndex; //11- Seta o indice do dropdown para o indice da resolução atual.
        resolutionsDropdown.RefreshShownValue(); //12- Atualiza o valor atualmente sendo apresentado no dropdown.
    }

    public void SetResolution(int resolutionIndex) //Resolução 
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex) //Quality 
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVSync(int vSyncIndex) //V-Sync 
    {
        if (vSyncIndex == 0)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public void SetAntiAliasing(int antiAliasingIndex) //Anti-Aliasing 
    {
        switch (antiAliasingIndex)
        {
            case 0:
                mainPostProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;
            case 1:
                mainPostProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
                break;
            case 2:
                mainPostProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
                break;
            case 3:
                mainPostProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
                break;
            default:
                mainPostProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;

        }
        antiAliasingSelector.index = antiAliasingIndex;
    }

    public void SetMotionBlur(int motionBlurIndex) //Motion Blur 
    {
        if (motionBlurIndex == 0)
        {
            mainPostProcessProfile.GetSetting<MotionBlur>().active = true;
        }
        else
        {
            mainPostProcessProfile.GetSetting<MotionBlur>().active = false;
        }

        motionBlurSelector.index = motionBlurIndex;
    }

    public void SetAmbientOclusion(int ambientOclusionIndex) //Ambient Oclusion 
    {
        if (ambientOclusionIndex == 0)
        {
            mainPostProcessProfile.GetSetting<AmbientOcclusion>().active = true;
        }
        else
        {
            mainPostProcessProfile.GetSetting<AmbientOcclusion>().active = false;
        }

        ambientOclusionSelector.index = ambientOclusionIndex;
    }

    public void SetBloom(int bloomIndex) //Bloom 
    {
        if (bloomIndex == 0)
        {
            mainPostProcessProfile.GetSetting<Bloom>().active = true;
        }
        else
        {
            mainPostProcessProfile.GetSetting<Bloom>().active = false;
        }
        bloomSelector.index = bloomIndex;
    }

    public void SetDephOfField(int depthOfFieldIndex) //Depth Of Field 
    {
        if (depthOfFieldIndex == 0)
        {
            mainPostProcessProfile.GetSetting<DepthOfField>().active = true;
        }
        else
        {
            mainPostProcessProfile.GetSetting<DepthOfField>().active = false;
        }
        depthOfFieldSelector.index = depthOfFieldIndex;
    }

    public void SetChromatic(int chromaticIndex) //Chromatic Aberration 
    {
        if (chromaticIndex == 0)
        {
            mainPostProcessProfile.GetSetting<ChromaticAberration>().active = true;
        }
        else
        {
            mainPostProcessProfile.GetSetting<ChromaticAberration>().active = false;
        }
        ChromaticSelector.index = chromaticIndex;
    }

    private void GraficCard() //pega o nome e o tamanho da memoria da placa de video e seta na variavel de texto nomeada graficsCardText.text 
    {
        if (SystemInfo.graphicsDeviceName != null)
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
        PlayerPrefs.SetInt("Monitor", monitorSelector.index);
        PlayerPrefs.SetInt("Resolution", resolutionsDropdown.value);
        PlayerPrefs.SetInt("Quality", qualitySelector.index);
        PlayerPrefs.SetInt("V-Sync", vSyncSelector.index);
        PlayerPrefs.SetInt("Anti-Aliasing", antiAliasingSelector.index);
        PlayerPrefs.SetInt("MotionBlur", motionBlurSelector.index);
        PlayerPrefs.SetInt("AmbientOclusion", ambientOclusionSelector.index);
        PlayerPrefs.SetInt("Bloom", bloomSelector.index);
        PlayerPrefs.SetInt("DepthOfField", depthOfFieldSelector.index);
        PlayerPrefs.SetInt("ChromaticAberration", ChromaticSelector.index);

        //Som
        audioMixer.GetFloat("VolumePrincipal", out volumePrincipal);    //pega o Exposed Parameter nomeado "VolumePrincipal" do audioMixer e manda para a variavel local de nome volumePrincipal 
        audioMixer.GetFloat("VolumeSFX", out volumeSFX);
        audioMixer.GetFloat("VolumeMusica", out volumeMusica);

        PlayerPrefs.SetFloat("VolumePrincipal", volumePrincipal);   //Cria um chave de nome "VolumePrincipal" e salva o valor contido na variavel volumePrincipal nela
        PlayerPrefs.SetFloat("VolumeSFX", volumeSFX);
        PlayerPrefs.SetFloat("VolumeMusica", volumeMusica);


        PlayerPrefs.Save();
        Debug.Log("Configurações salvas!");

        GameManager.gameManager.hasSelected = false;


    }


    #region Troca de Paineis

    //   ***
    //Configurações de movimentação entre paineis
    //   ***




    //troca os painels usando os botões Q E
    private void ChangeOptionsPanelsByButton()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            indexPanel++;
            if (indexPanel > optionsPanels.Length - 1)
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
                indexPanel = optionsPanels.Length - 1;
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
        GameManager.gameManager.hasSelected = false;
    }

    #endregion
}
