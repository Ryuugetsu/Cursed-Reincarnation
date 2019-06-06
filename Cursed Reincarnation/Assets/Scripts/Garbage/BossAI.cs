using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public float lookRadius = 20f;
    Transform player;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        //player = PlayerManager.instance.player.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        
        if(distance <= lookRadius)
        {
            agent.SetDestination(player.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}