using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HappyMovement : MonoBehaviour
{
    public Transform barajas;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = barajas.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
