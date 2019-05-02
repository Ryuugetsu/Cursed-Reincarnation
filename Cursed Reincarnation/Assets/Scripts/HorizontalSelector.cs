using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HorizontalSelector : MonoBehaviour, ISelectHandler
{
    public Button buttonRight;
    private LayoutElement buttonRightLayoutElement;
    public Button buttonLeft;
    private LayoutElement buttonLeftLayoutElement;

    public Button mainButton;

    [SerializeField]
    private TextMeshProUGUI text = null;

  
    public int index = 0;
    //public int defaultIndex = 0;

    public List<string> data = new List<string>();
    
    

    // Start is called before the first frame update
    void Start()
    {
        //pega os botões direito e esquerdos, armazena o componente tipo LayoutElement numa variavel e a da um Set no flexibleWidith como 0 (isso faz que a largura se ajuste ao conteudo);
        buttonRightLayoutElement = buttonRight.GetComponent<LayoutElement>();
        buttonRightLayoutElement.flexibleWidth = 0;

        buttonLeftLayoutElement = buttonLeft.GetComponent<LayoutElement>();
        buttonLeftLayoutElement.flexibleWidth = 0;

        buttonLeft.onClick.AddListener(OnLeftClicked); //função do botão para esperar um click
        buttonRight.onClick.AddListener(OnRightClicked);//função do botão para esperar um click

        //index = defaultIndex;
        text.text = data[index];

    }

    void Update()
    {
        //mudar o preferredwidth(lagura) do botão para que ele tenha sempre o mesmo tamanhos da altura do botão (formando um quadrado)
        buttonRightLayoutElement.preferredWidth = buttonRight.GetComponent<RectTransform>().rect.height;
        buttonLeftLayoutElement.preferredWidth = buttonLeft.GetComponent<RectTransform>().rect.height;
    }

    //ação executada ao clicar na seta da esquerda (mudar o index da lista)
    public void OnLeftClicked()
    {
        if(index == 0)
        {
            index = 0;
        }
        else
        {
            index--;
        }
        text.text = data[index];
    }

    //ação executada ao clicar na seta da direita
    void OnRightClicked()
    {
        if((index + 1) >= data.Count)
        {
            index = data.Count-1;
        }
        else
        {
            index++;
        }
        text.text = data[index];
    }

    //metodo criado para caso esteja usando botoes de navegação, avisar ao game manager
    public void OnSelect(BaseEventData eventData)
    {
        GameManager.gameManager.hasSelected = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnLeftClicked();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRightClicked();
        }
    }

    

}
