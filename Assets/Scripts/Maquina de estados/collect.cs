using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect : StateMachineBehaviour
{
    public int conteo;
    public float secCollect;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al entrar te suma uno al conteo de objetos
        conteo++;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Que espere 3 segundos, si tiene 3 o mas de inventario que se vaya a base, sino, que siga buscando
        secCollect = secCollect + 1 * Time.deltaTime;
        if (secCollect >= 3)
        {
            if(conteo >= 3)
                {
                animator.SetBool("base", true);
                //conteo = 0;
            }
            else
            {
                conteo++;
                animator.SetBool("collect", false);
            }
            
            

        }
        
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        secCollect = 0;
        //En caso de stun
        animator.SetBool("collect", false);
        animator.SetBool("scan", false);
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
