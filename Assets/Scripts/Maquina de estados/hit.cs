using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hit : StateMachineBehaviour
{

    private NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Ray ray = new Ray(agent.transform.position, agent.transform.forward);
        Debug.DrawRay(agent.transform.position, agent.transform.forward * 1f, Color.red);
        RaycastHit toca;
        if (Physics.Raycast(ray, out toca, 1f))
        {
            if (toca.transform.tag == "Rover")
            {
                animator.GetBehaviour<collect>().conteo = animator.GetBehaviour<collect>().conteo + toca.transform.GetComponent<Animator>().GetBehaviour<collect>().conteo;
                toca.transform.GetComponent<Animator>().GetBehaviour<collect>().conteo = 0;
                toca.transform.GetComponent<Animator>().SetBool("hit", true);

                animator.SetBool("base", true);
            }
            //agent.transform.position = toca.transform.position;


        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
