using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scan : StateMachineBehaviour
{
    private NavMeshAgent agent;
    public float secScan;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        secScan = 0;
        agent = animator.gameObject.GetComponent<NavMeshAgent>();

        agent.speed = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = 0f;
        
        secScan = secScan + 1 * Time.deltaTime;
        if (secScan >= 5)
        {
            secScan = 0;
            animator.SetBool("scan", false);

        }


        Ray ray = new Ray(agent.transform.position, agent.transform.forward);
        Debug.DrawRay(agent.transform.position, agent.transform.forward, Color.red);
        RaycastHit toca;
        if (Physics.Raycast(ray, out toca, 5f))
        {
            


            if (toca.transform.tag == ("roca"))
            {
                
            }

            //animator.SetBool("scan", false);
            //hit.transform.tag = 
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

   
   
}
