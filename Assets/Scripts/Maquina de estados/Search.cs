using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Search : StateMachineBehaviour
{
    public Transform[] barajas;
    public int numPoint = 0;
    public float velocity = 5f;
    public NavMeshAgent agent;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        //le decimos que siga la ruta de los arrays
        agent.destination = barajas[numPoint].position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Aquí le digo que cada vez que llegue pase al siguiente punto, y si el punto es el máximo, que vuelva al principio

        if (Vector3.Distance(animator.gameObject.transform.position, barajas[numPoint].position) < 1f)
        {
            agent.destination = barajas[numPoint].position;
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
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
