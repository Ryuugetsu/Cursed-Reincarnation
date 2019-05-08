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
    public HorizontalSelector resolutionsSelector;
    public HorizontalSelector vSyncSelector;
    public HorizontalSelector antiAliasingSelector;
    public HorizontalSelector motionBlurSelector;
    public HorizontalSelector ambientOclusionSelector;
    public HorizontalSelector bloomSelector;
    public HorizontalSelector depthOfFieldSelector;
    public HorizontalSelector ChromaticSelector;
    public HorizontalSelector shadowsSelector;

    public bool loadingConfig = false;

    [SerializeField] private GameObject[] optionsPanels = null;
    private int indexPanel = 0;
    [SerializeField] private GameObject applyPanel = null;
    public bool hasChanges = false;
    [SerializeField] private GameObject mainMenu = null;

    #region Metodo que manipula a Posição da Janela

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
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        mainPostProcessLayer = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessLayer>();
        

        GraficCard(); //chama o metodo de pegar o nome da placa de video da maquina em que o jogo esta sendo executado

        //verifica quantos monitores estão conectados e alimenta o MonitorHorizontalSelector
        Debug.Log("displays connected: " + Display.displays.Length);
        monitorSelector.textDropdown.ClearOptions();
        List<string> monitores = new List<string>();
        for (int i = 0; i < Display.displays.Length; i++)
        {
            monitores.Add("Monitor " + (i + 1));
        }
        monitorSelector.textDropdown.AddOptions(monitores);
        monitorSelector.textDropdown.RefreshShownValue();

        LoadConfig();

    }


    // Update is called once per frame
    void Update()
    {
        ChangeOptionsPanelsByButton();
        if (hasChanges == true && Input.GetKeyDown(KeyCode.Escape))
        {
            applyPanel.SetActive(true);
        }
        else if (hasChanges == false && Input.GetKeyDown(KeyCode.Escape))
        {
            applyPanel.SetActive(false);
            CloseMenuConfig();
        }
    }

    #region Grafics Setings

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
        hasChanges = true;
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
                    monitorSelector.textDropdown.value = 0;
                    //SetPosition(0, 0);
                    monitorSelector.OnLeftClicked();
                }

            }

        }
        hasChanges = true;
    }

    IEnumerator OnMonitorSwith()
    {
        if (Screen.fullScreen == false)
        {
            Screen.fullScreen = true;
            yield return new WaitForSeconds(0.1f);
            Screen.fullScreen = false;
            GetResolutions();
            resolutionsSelector.textDropdown.value = resolutionsSelector.textDropdown.value - 1;            

        }
        else
        {
            GetResolutions();
        }
    }

    public void GetResolutions() //Metodo que alimenta o resolutionsDropdown com as resoluções compativeis com seu monitor. 
    {
        resolutions = Screen.resolutions; //1- Alimenta o Vector resoltion com todas as resoluções compativeis com o monitor.

        resolutionsSelector.textDropdown.ClearOptions(); //2- Apaga o conteudo atual do dropdown.

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
        resolutionsSelector.textDropdown.AddOptions(options); //7- Alimenta o dropdown com os valores da lista de strings.
        resolutionsSelector.textDropdown.value = currentResolutinIndex; //11- Seta o indice do dropdown para o indice da resolução atual.
        resolutionsSelector.textDropdown.RefreshShownValue(); //12- Atualiza o valor atualmente sendo apresentado no dropdown.

        loadingConfig = true;
    }

    public void SetResolution(int resolutionIndex) //Resolução 
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        hasChanges = true;
    }

    public void SetQuality(int qualityIndex) //Quality 
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        hasChanges = true;
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
        hasChanges = true;
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
        hasChanges = true;
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
        hasChanges = true;
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
        hasChanges = true;
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
        hasChanges = true;
    }

    public void SetDepthOfField(int depthOfFieldIndex) //Depth Of Field 
    {
        if (depthOfFieldIndex == 0)
        {
            mainPostProcessProfile.GetSetting<DepthOfField>().active = true;
        }
        else
        {
            mainPostProcessProfile.GetSetting<DepthOfField>().active = false;
        }
        hasChanges = true;
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
        hasChanges = true;
    }

    public void SetShadows(int shadowIndex)
    {
        if (shadowIndex == 1)
        {
            QualitySettings.shadows = ShadowQuality.HardOnly;
        }else if (shadowIndex == 2)
        {
            QualitySettings.shadows = ShadowQuality.All;
        }
        else
        {
            QualitySettings.shadows = ShadowQuality.Disable;
        }
        hasChanges = true;
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

    #endregion


    #region Sound Settings

    //configurações de som
    public void SetVolumePrincipal(float volume)
    {
        audioMixer.SetFloat("VolumePrincipal", volume);
        hasChanges = true;
    }
    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("VolumeSFX", volume);
        hasChanges = true;
    }
    public void SetVolumeMusica(float volume)
    {
        audioMixer.SetFloat("VolumeMusica", volume);
        hasChanges = true;
    }

    #endregion


    #region Save and Load Settings

    //Apply window config
    public void Apply(bool apply)
    {

        if (apply == true)
        {
            SaveConfig();
        }
        else
        {
            LoadConfig();
        }
        hasChanges = false;
        applyPanel.SetActive(false);
        CloseMenuConfig();

    }
    
    //Save Settings in playerprefs
    private void SaveConfig()
    {
        float volumePrincipal = 0;
        float volumeSFX = 0;
        float volumeMusica = 0;

        //graficos
        PlayerPrefs.SetInt("Fullscreen", fullscreenSelector.textDropdown.value);
        PlayerPrefs.SetInt("Monitor", monitorSelector.textDropdown.value);
        PlayerPrefs.SetInt("Resolution", resolutionsSelector.textDropdown.value);
        PlayerPrefs.SetInt("Quality", qualitySelector.textDropdown.value);
        PlayerPrefs.SetInt("V-Sync", vSyncSelector.textDropdown.value);
        PlayerPrefs.SetInt("Anti-Aliasing", antiAliasingSelector.textDropdown.value);
        PlayerPrefs.SetInt("MotionBlur", motionBlurSelector.textDropdown.value);
        PlayerPrefs.SetInt("AmbientOclusion", ambientOclusionSelector.textDropdown.value);
        PlayerPrefs.SetInt("Bloom", bloomSelector.textDropdown.value);
        PlayerPrefs.SetInt("DepthOfField", depthOfFieldSelector.textDropdown.value);
        PlayerPrefs.SetInt("ChromaticAberration", ChromaticSelector.textDropdown.value);
        PlayerPrefs.SetInt("Shadows", shadowsSelector.textDropdown.value);

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
       
    //Load Settings
    public void LoadConfig()
    {

        //load configurações gráficas
        fullscreenSelector.textDropdown.value = PlayerPrefs.GetInt("Fullscreen", 0);
        monitorSelector.textDropdown.value = PlayerPrefs.GetInt("Monitor", 0);
        //menuConfigComp.GetResolutions();
        //menuConfigComp.resolutionsSelector.textDropdown.value = PlayerPrefs.GetInt("Resolution", 0);
        StartCoroutine(loadResolution());
        qualitySelector.textDropdown.value = PlayerPrefs.GetInt("Quality", 0);
        vSyncSelector.textDropdown.value = PlayerPrefs.GetInt("V-Sync", 0);
        antiAliasingSelector.textDropdown.value = PlayerPrefs.GetInt("Anti-Aliasing", 0);
        motionBlurSelector.textDropdown.value = PlayerPrefs.GetInt("MotionBlur", 0);
        ambientOclusionSelector.textDropdown.value = PlayerPrefs.GetInt("AmbientOclusion", 0);
        bloomSelector.textDropdown.value = PlayerPrefs.GetInt("Bloom", 0);
        depthOfFieldSelector.textDropdown.value = PlayerPrefs.GetInt("DepthOfField", 0);
        ChromaticSelector.textDropdown.value = PlayerPrefs.GetInt("ChromaticAberration", 0);
        shadowsSelector.textDropdown.value = PlayerPrefs.GetInt("Shadows", 0);

        //load configurações de som
        sliderVolumePrincipal.value = PlayerPrefs.GetFloat("VolumePrincipal", 0);
        sliderVolumeSFX.value = PlayerPrefs.GetFloat("VolumeSFX", 0);
        sliderVolumeMusica.value = PlayerPrefs.GetFloat("VolumeMusica", 0);


        //refresh all 
        HorizontalSelector[] refresh = GetComponentsInChildren<HorizontalSelector>();
        foreach (HorizontalSelector r in refresh)
        {
            r.textDropdown.RefreshShownValue();
        }
        Debug.Log("Configurações Caregadas!");
    }
    IEnumerator loadResolution()
    {
        loadingConfig = false;
        GetResolutions();
        yield return new WaitUntil(() => loadingConfig == true);
        loadingConfig = false;
        resolutionsSelector.textDropdown.value = PlayerPrefs.GetInt("Resolution", 0);
    }

    #endregion


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

    private void CloseMenuConfig()
    {
        if(mainMenu != null)
        {
            mainMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        hasChanges = false;
        GameManager.gameManager.hasSelected = false;
    }
}
