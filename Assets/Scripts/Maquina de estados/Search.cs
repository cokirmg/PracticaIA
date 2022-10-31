using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Search : StateMachineBehaviour
{
    //public Transform[] barajas;
    public int numPoint ;
    //public float velocity = 5f;
    public NavMeshAgent agent;
    private Transform[] barajas;
    public SearchPoints objetivo;
    public float secCharge;


    public Transform objetoRaycast;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        barajas = animator.gameObject.GetComponent<SearchPoints>().barajasPoints;
        objetivo = animator.gameObject.GetComponent<SearchPoints>();
        //numPoint = animator.gameObject.GetComponent<SearchPoints>().numPoint;

        secCharge = 0;
        agent.speed = 3.5f;
        //agent.destination = barajas[numPoint].position;
        //le decimos que siga la ruta de los arrays
        //agent.destination = barajas[numPoint].position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.destination = barajas[numPoint].position;
        //Aquí le digo que cada vez que llegue pase al siguiente punto, y si el punto es el máximo, que vuelva al principio

        if (Vector3.Distance(agent.transform.position, barajas[numPoint].position) < 1f)
        {
            agent.destination = barajas[numPoint].position;
            Debug.Log("punto " + numPoint);
            numPoint = (numPoint + 1) % barajas.Length;


        }


        agent.destination = barajas[numPoint].position;


        

        //Si toca la arena, que se reduzca la velocidad a la mitad
        int sandMask = 1 << NavMesh.GetAreaFromName("Sand");
        NavMeshHit hit;
        if (NavMesh.SamplePosition(animator.gameObject.transform.position, out hit, 0.01f, sandMask))
        {
            //Aqui le digo que solo lo haga si su velocidad es mayor que la mitad de su velocidad normal, para que no lo haga todo el rato
            if (agent.speed > agent.speed / 2)
            {
                Debug.Log("arena");
                agent.speed = agent.speed / 2;
            }

        }
        else //Si no toca la arena que sea su velocidad normal
        {
            agent.speed = 3.5f;
        }

        secCharge = secCharge + 1 * Time.deltaTime;
        if (secCharge >= 30)
        {
            secCharge = 0;
            animator.SetBool("charge", true);
            
        }

        Ray ray = new Ray(agent.transform.position, agent.transform.forward);
        Debug.DrawRay(agent.transform.position, agent.transform.forward*5f, Color.red);
        RaycastHit toca;
        
            if (Physics.Raycast(ray, out toca, 5f))
            {
                if (objetoRaycast != toca.transform) {
                    if (!(toca.transform.tag == ("pared")))
                    {
                        if (agent.transform.name == "Grumpy" && toca.transform.tag == ("Rover"))
                        {
                        
                            objetivo.objetivo = toca.transform;
                            agent.destination = objetivo.objetivo.transform.position;
                        Debug.Log(objetivo); 
                        //agent.destination = toca.transform.position;
                        animator.SetBool("follow", true); 
                        
                        
                        }
                        else
                        {
                        
                            animator.SetBool("scan", true);
                      

                         }
                            objetoRaycast = toca.transform;


                    }


                }
            }
        


        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = 0f;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
