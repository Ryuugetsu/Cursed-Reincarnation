using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemy;
    //public Transform enemyPos;
    public float xPos;
    public float zPos;
    private float repeatRate = 5.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 0.5f, repeatRate); //repete a "invocação" 
            Destroy(gameObject, 11); // destroi o gameobject responsavel por criar inimigos para limitar o numero de inimigos
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        xPos = Random.Range(-15.99f, -22.68f);
        zPos = Random.Range(-34.86f, -42.251f);
        Instantiate(enemy, new Vector3(xPos,0,zPos), Quaternion.Euler(new Vector3(0, 180, 0)));
        //Instantiate(enemy, enemyPos.position, enemyPos.rotation);
    }
}
