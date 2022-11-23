using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class waiting : StateMachineBehaviour
{

    private NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 0f; //Hace que se pare el rover
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = 3.5f; //Vuelve a darle velocidad
    }
    public void vaciarInventario()
    {
        //Si se encuentra en el are de Base le borra el inventario

        int baseMask = 1 << NavMesh.GetAreaFromName("Base");
        NavMeshHit hit;

        if (!(NavMesh.SamplePosition(agent.gameObject.transform.position, out hit, 1f, baseMask)))
        {

            agent.transform.GetComponent<Animator>().GetBehaviour<collect>().conteo = 0;

        }
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
