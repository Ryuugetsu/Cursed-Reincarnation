using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public bool isGrounded; //Varivel bolleana de teste usada para verificar se o personagem está no chão
    public bool isCrouching; //Varivel bolleana de teste usada para verificar se o personagem está no abaixado

    [SerializeField] private float speed; //velocidade do estado atual (andando, correndo ou abaixado)
    [SerializeField] private float w_speed = 0.05f; //Velocidade Andando
    [SerializeField] private float r_speed = 0.1f; //Velocidade Correndo
    [SerializeField] private float h_speed = 0.2f; //Velocidade Correndo mais rápido
    [SerializeField] private float c_speed = 0.025f; //Velocidade Abaixado
    [SerializeField] private float rotSpeed = 3.0f; //Velocidade de rotacionamente
    [SerializeField] private float jumpHeight = 135.0f; //Altura do pulo

    Rigidbody rb; //componente RigidyBody do Player
    Animator anim; 
    CapsuleCollider col;

    public float hor; //variavel que receberá o input Horizontal
    public float ver; //variavel que receberá o input Vertical

    //novo - atalhos para testar mouse buttons etc
    public KeyCode attack1;
    public KeyCode attack2;


    //novo - vida do player p parar as animações enquanto não temos tela de game over
    public Slider healthbar;

    [SerializeField] private Transform cameraBase;

    public bool Equiped = false;
    [SerializeField] public GameObject item1;
    public bool showItem;



    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        isGrounded = true;
        showItem = false;
        Cursor.lockState = CursorLockMode.Locked; //travar e destravar o cursor durante o jogo
	}
	
	// Update is called once per frame
	void Update () {

        // Impedir funcionamento das ações caso a vida esteja 0
        if(healthbar.value <= 0) return;

        PlayerMovement();

        Crounch();
        Jump();
        Combat();
        MovementAnimation();
        EquipWeapon();

        if(Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

	}

    void PlayerMovement()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        if (hor > 0.3 || hor < -0.3 || ver > 0.3 || ver < -0.3)
        {
            Vector3 playerMovement = new Vector3(hor, 0, ver) * speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, cameraBase.rotation.eulerAngles.y, 0);            
            transform.Translate(playerMovement, Space.Self);

            anim.SetFloat("velX", hor);
            anim.SetFloat("velY", ver);
        }
        
    }
    
    void Crounch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                isCrouching = false;
                anim.SetBool("isCrouching", false);
                col.height = 2;
                col.center = new Vector3(0, 1, 0);
            }

            else
            {
                isCrouching = true;
                anim.SetBool("isCrouching", true);
                speed = c_speed;
                col.height = 1;
                col.center = new Vector3(0, 0.5f, 0);
            }
        }
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(0, jumpHeight, 0);
            anim.SetTrigger("isJumping");
            isCrouching = false;
            isGrounded = false;
        }
    }

    void EquipWeapon()
    {
        if (showItem == false)
        {
            item1.SetActive(false);
        }
        if (showItem == true)
        {
            item1.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && showItem == false)
        {
            anim.SetTrigger("toEquip");
            showItem = true;
            anim.SetBool("isEquiped", true);
            Equiped = true;
            

        }

        if (Input.GetKeyDown(KeyCode.R) && showItem == true)
        {
            anim.SetTrigger("toUnEquip");
            showItem = false;
            anim.SetBool("isEquiped", false);
            Equiped = false;
        }
        
        else {
            anim.SetBool("isEquiped", false);
        }

    }

    void Combat()
    {
        //soco simples

        if (Input.GetKeyDown(attack2) && isGrounded == true)
        {
            anim.SetBool("attacking2", true);
        }
        else
        {
            anim.SetBool("attacking2", false);
        }


        //golpe de adaga

        if (Input.GetKeyDown(attack1) && isGrounded == true)
        {
           anim.SetBool("attacking1", true);
        }
        else
        {
            anim.SetBool("attacking1", false);
        }

    }

    void MovementAnimation()
    {
        if (isCrouching)
        {//abaixado
            if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }

        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = r_speed;
            //correr
            if ( Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }



            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }

        else if (!isCrouching)
        {
            speed = w_speed;
            //de pé
            if ( Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D))
            {
               // isGrounded = true;
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }

            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
    }

    void OnCollisionEnter()
    {
        isGrounded = true;
    }


}

	