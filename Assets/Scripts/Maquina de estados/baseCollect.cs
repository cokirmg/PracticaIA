using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class baseCollect : StateMachineBehaviour
{
    public float secBase;
    private NavMeshAgent agent;
    private Transform basePoint;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("collect", false);
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        basePoint = animator.gameObject.GetComponent<SearchPoints>().basePoint;
        //la velocidad pasa a estar normal despues del scan
        agent.speed = 3.5f;
        secBase = 0f;
        

        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        //Espera 3 segundos
        secBase = secBase + 1 * Time.deltaTime;
        if (secBase >= 3)
        {

            //Cuando llega a base el conteo pasa a 0 y cambia de estado a search
            if (Vector3.Distance(agent.transform.position, basePoint.position) < 1f)
            {
                animator.transform.GetComponent<Animator>().GetBehaviour<collect>().conteo = 0;
                animator.SetBool("base", false);


            }
            //Si no esta cerca del waypoint de search que siga al destino a la base
            agent.destination = basePoint.position;
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //En caso de que le stuneen que se quite el true del base
        animator.SetBool("base", false);
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
