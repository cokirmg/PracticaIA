using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HappyMovement : MonoBehaviour
{
    
    //Array de los waypoints;
    public Transform[] barajas;
    public int numPoint = 0;
    public float velocity = 5f;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
       
        agent = GetComponent<NavMeshAgent>();
        //le decimos que siga la ruta de los arrays
        agent.destination = barajas[numPoint].position;

    }

    // Update is called once per frame
    void Update()
    {
        //Aquí le digo que cada vez que llegue pase al siguiente punto, y si el punto es el máximo, que vuelva al principio
        if (Vector3.Distance(transform.position, barajas[numPoint].position) < 1f)
        {
            if(numPoint == barajas.Length)
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
            
        }

        //Si toca la arena, que se reduzca la velocidad a la mitad
        int sandMask = 1 << NavMesh.GetAreaFromName("Sand");
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 0.01f, sandMask))
        {
            //Aqui le digo que solo lo haga si su velocidad es mayor que la mitad de su velocidad normal, para que no lo haga todo el rato
            if(agent.speed > agent.speed / 2)
            {
                Debug.Log("arena");
                agent.speed = agent.speed / 2;
            }
            
        }
        else //Si no toca la arena que sea su velocidad normal
        {
            agent.speed = 3.5f;
        }
    }
}

