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
    private TextMeshProUGUI text;

  
    public int index = 0;
    public int defaultIndex = 0;

    public List<string> data = new List<string>();
    
    

    // Start is called before the first frame update
    void Start()
    {
        //pega os botões direito e esquerdos, armazena o componente tipo LayoutElement numa variavel e a da um Set no flexibleWidith como 0;
        buttonRightLayoutElement = buttonRight.GetComponent<LayoutElement>();
        buttonRightLayoutElement.flexibleWidth = 0;

        buttonLeftLayoutElement = buttonLeft.GetComponent<LayoutElement>();
        buttonLeftLayoutElement.flexibleWidth = 0;

        buttonLeft.onClick.AddListener(OnLeftClicked);
        buttonRight.onClick.AddListener(OnRightClicked);

        index = defaultIndex;
        text.text = data[index];

    }

    // Update is called once per frame
    void Update()
    {
        //mudar o preferredwidth(lagura) do botão para que ele tenha sempre o mesmo tamanhos da altura do botão (formando um quadrado)
        buttonRightLayoutElement.preferredWidth = buttonRight.GetComponent<RectTransform>().rect.height;
        buttonLeftLayoutElement.preferredWidth = buttonLeft.GetComponent<RectTransform>().rect.height;
    }
    
    void OnLeftClicked()
    {
        if(index == 0)
        {
            index = data.Count - 1;
        }
        else
        {
            index--;
        }
        text.text = data[index];
    }
    void OnRightClicked()
    {
        if((index + 1) >= data.Count)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        text.text = data[index];
    }

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
    public void OnDeselect()
    {
        StartCoroutine(Deselect());
    }
    private IEnumerator Deselect()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.gameManager.hasSelected = false;
    }

    

}
