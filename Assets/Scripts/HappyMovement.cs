using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HappyMovement : MonoBehaviour
{
    
    //public Transform[] barajas;
    public Transform[] barajas;
    public int numPoint = 0;
    public float velocity = 5f;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
       
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = barajas.position;
        agent.destination = barajas[numPoint].position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, barajas[numPoint].position) < 1f)
        {
            if(numPoint >= 6)
            {
                numPoint = 0;
            }
            else
            {
                numPoint++;
                
            }
            agent.destination = barajas[numPoint].position;
        }
        else
        {
            agent.destination = barajas[numPoint].position;
            //GetComponent<Rigidbody>().velocity = (barajas[numPoint].position - transform.position).normalized * velocity;
        }

        // Find the nearest point on water.
        int sandMask = 1 << NavMesh.GetAreaFromName("Sand");
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2.0f, sandMask))
        {
            agent.speed = 1.5f;
        }
        else
        {
            agent.speed = 3.0f;
        }
    }
}

