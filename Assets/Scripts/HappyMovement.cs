using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HappyMovement : MonoBehaviour
{
    public Vector3 values;
    //public Transform[] barajas;
    public Transform barajas;
    public int num = 0;

    // Start is called before the first frame update
    void Start()
    {
       // barajas = Transform.
        //values[num] = barajas.Transform.position;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = barajas.position;

       // agent.destination = new Vector3[barajas.position[num]];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
