using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject enemy;
    public GameObject spawnTrigger;
    public Vector3 center;
    public Vector3 size;
    //private float sec = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("Spawner", 1, 2); // função / delay / num de inimigos
            Destroy(gameObject, 6); // destroi o spawntrigger
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //StartCoroutine(Delay());
        }   
    }

    /*
    IEnumerator Delay()
    {
   
        yield return new WaitForSeconds(sec);
        gameObject.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }*/

    void Spawner()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x/2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Vector3 rot = new Vector3(0, 60, 0);
        Instantiate(enemy, pos, Quaternion.Euler(rot));
     
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.1f);
        Gizmos.DrawCube(center, size);
    }

}
