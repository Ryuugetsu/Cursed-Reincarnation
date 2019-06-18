using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSpawn : MonoBehaviour
{

    public GameObject enemy;
    public Transform spawnPosition;
   // public GameObject spawnTrigger;
    public Vector3 center;
    public Vector3 size;
    private float repeatRate = 5.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("Spawner", 0.5f, repeatRate); // função / delay / num de inimigos
            Destroy(gameObject, 6); // destroi o spawntrigger
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }



    void Spawner()
    {
        Instantiate(enemy, spawnPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)));

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawCube(center, size);
    }

}
