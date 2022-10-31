using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class follow : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private SearchPoints agentFollow;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        agentFollow = animator.gameObject.GetComponent<SearchPoints>();
        
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        agent.destination = agentFollow.objetivo.transform.position;
        agent.speed = 3.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        agent.speed = 3.5f;
        agent.destination = agentFollow.objetivo.transform.position;

        Ray ray = new Ray(agent.transform.position, agent.transform.forward);
        Debug.DrawRay(agent.transform.position, agent.transform.forward * 1f, Color.red);
        RaycastHit toca;
        if (Physics.Raycast(ray, out toca, 1f))
        {
            if(toca.transform.tag == "Rover")
            {
               animator.SetBool("follow", false);
                
            }
            //agent.transform.position = toca.transform.position;

            
        }
        else
        {
            agent.destination = agentFollow.objetivo.transform.position;
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
