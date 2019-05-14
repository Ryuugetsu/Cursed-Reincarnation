using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(InputTest());

    }

    IEnumerator InputTest()
    {
        while (true)
        {
            if(Input.anyKeyDown)
            Debug.Log(Event.current);
            yield return new WaitForSeconds(2);

        }

    }


    public enum InputType
    {
        //tipos de input
        MouseKeyboard,
        Controler
    };

    //m_state é o atual tipo de input
    public InputType inputState = InputType.MouseKeyboard;


    void OnGUI()
    {
        switch (inputState)
        {
            case InputType.MouseKeyboard:               //caso o tipo de input atual for Mouse ou Keyboard
                if (isControlerInput())                 //se receber um input do tipo Controller
                {
                    inputState = InputType.Controler;      //então troca o tipo de input atual para Controller
                    Debug.Log("Joysyick");
                }
                break;
            case InputType.Controler:                   //caso o tipo de input atual for Controller
                if (isMouseKeyboard())                      //se receber um input do tipo Mouse ou Keyboard
                {
                    inputState = InputType.MouseKeyboard;  //então troca o tipo de input atual para Mouse ou Keyboard
                    Debug.Log("Mouse/Keyboard");
                }
                break;
        }
    }


    //metodo que retorna o tipo de input atual
    public InputType GetInputState()
    {
        return inputState;
    }

    //Metodo booleano que rotarna true, se o input veio do Teclado ou Mouse
    private bool isMouseKeyboard()
    {
        // mouse & keyboard buttons
        if (Event.current.isKey || Event.current.isMouse)
        {
            return true;
        }

        // mouse movement
        if (Input.GetAxis("Mouse X") != 0.0f ||
            Input.GetAxis("Mouse Y") != 0.0f)
        {
            return true;
        }
        return false;
    }
    
    //Metodo booleano que rotarna true, se o input veio do Controle / Joypad
    private bool isControlerInput()
    {
        // joystick buttons
        if (Input.GetKey(KeyCode.JoystickButton0) ||
           Input.GetKey(KeyCode.JoystickButton1) ||
           Input.GetKey(KeyCode.JoystickButton2) ||
           Input.GetKey(KeyCode.JoystickButton3) ||
           Input.GetKey(KeyCode.JoystickButton4) ||
           Input.GetKey(KeyCode.JoystickButton5) ||
           Input.GetKey(KeyCode.JoystickButton6) ||
           Input.GetKey(KeyCode.JoystickButton7) ||
           Input.GetKey(KeyCode.JoystickButton8) ||
           Input.GetKey(KeyCode.JoystickButton9) ||
           Input.GetKey(KeyCode.JoystickButton10) ||
           Input.GetKey(KeyCode.JoystickButton11) ||
           Input.GetKey(KeyCode.JoystickButton12) ||
           Input.GetKey(KeyCode.JoystickButton13) ||
           Input.GetKey(KeyCode.JoystickButton14) ||
           Input.GetKey(KeyCode.JoystickButton15) ||
           Input.GetKey(KeyCode.JoystickButton16) ||
           Input.GetKey(KeyCode.JoystickButton17) ||
           Input.GetKey(KeyCode.JoystickButton18) ||
           Input.GetKey(KeyCode.JoystickButton19))
        {
            return true;
        }

        

        /*
        // joystick axis
        if (Input.GetAxis("Left Stick X Axis") != 0.0f ||
           Input.GetAxis("XC Left Stick Y") != 0.0f ||
           Input.GetAxis("XC Triggers") != 0.0f ||
           Input.GetAxis("XC Right Stick X") != 0.0f ||
           Input.GetAxis("XC Right Stick Y") != 0.0f)
        {
            return true;
        }*/

        return false;
    }

}
