using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class HorizontalSelector : MonoBehaviour, ISelectHandler
{
    public Button buttonRight;
    private LayoutElement buttonRightLayoutElement;
    public Button buttonLeft;
    private LayoutElement buttonLeftLayoutElement;

    public Button mainButton;

    public TMP_Dropdown textDropdown = null;

  
    //public int index = 0;

    
    

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
        if(textDropdown.value == 0)
        {
            textDropdown.value = 0;
        }
        else
        {
            textDropdown.value--;
        }
        //textDropdown.value = index;
    }

    //ação executada ao clicar na seta da direita
    void OnRightClicked()
    {
        if((textDropdown.value + 1) >= textDropdown.options.Count)
        {
            textDropdown.value = textDropdown.options.Count - 1;
        }
        else
        {
            textDropdown.value++;
        }
        //textDropdown.value = index;
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
