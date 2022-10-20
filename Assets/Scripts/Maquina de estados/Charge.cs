using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Charge : StateMachineBehaviour
{
    public bool charge;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        charge = animator.gameObject.GetComponent<chargeCooldown>().needCharge;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        charge = animator.gameObject.GetComponent<chargeCooldown>().needCharge;
        if (charge == false)
        {
            animator.SetBool("charge", false);
        }
    }

}
