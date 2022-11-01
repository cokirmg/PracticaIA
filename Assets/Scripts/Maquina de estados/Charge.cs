using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Charge : StateMachineBehaviour
{
    private NavMeshAgent agent;
    public float secCharging;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        secCharging = 0;
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 0f;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Suma el tiempo y si llega a 10 para de cargar y vuelve a search
        agent.speed = 0f;
        secCharging =secCharging + 1 * Time.deltaTime;
        if (secCharging >= 10)
        {
            secCharging = 0;
            animator.SetBool("charge", false);

        }
    }

}
