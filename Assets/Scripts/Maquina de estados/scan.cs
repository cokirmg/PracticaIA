using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scan : StateMachineBehaviour
{
    private NavMeshAgent agent;
    public float secScan;
    public float rotationNum = 20f;
    
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
        Ray ray = new Ray(agent.transform.position, agent.transform.forward);
        Debug.DrawRay(agent.transform.position, agent.transform.forward, Color.red);
        RaycastHit toca;

        //Si el agente es Happy o Grumpy
        if (agent.name == "Happy" || agent.name == "Grumpy")
        {
            if (agent.transform.rotation.y >= 0f && agent.name == "Happy")
            {      //Si es happyy y no ha completado el giro de 360 grados 
                agent.transform.Rotate(0, 90f * Time.deltaTime, 0);
                if (Physics.Raycast(ray, out toca, 5f))
                {   //Que si ve a un rover que pase a scan mientras sigue haciendo el giro
                    if (toca.transform.tag == "Rover")
                    {
                        animator.SetBool("scan", false);
                    }

                }
                else
                { //Si no ve a un rover escanea durante 5 segundo y si es una planta pasa a collect, sino a scan
                    secScan = secScan + 1 * Time.deltaTime;

                    if (secScan >= 5f)
                    {
                        if (Physics.Raycast(ray, out toca, 5f))
                        {
                            if (toca.transform.tag == ("planta"))
                            {
                                animator.SetBool("collect", true);
                            }
                            else
                            {
                                animator.SetBool("scan", false);
                            }
                        }

                    }
                    
                }
            }
            else if (agent.name == "Grumpy")
            {
                secScan = secScan + 1 * Time.deltaTime;
                //Si es grumpy escanea durante 5 segundos y si es una planta pasa a collect y sino a scan
                if (secScan >= 5f)
                {
                    if (Physics.Raycast(ray, out toca, 5f))
                    {
                        if (toca.transform.tag == ("planta"))
                        {
                            animator.SetBool("collect", true);
                        }
                        else
                        {
                            animator.SetBool("scan", false);
                        }

                    }

                }
                
            }

        }

        if (agent.name == "Dopey")
        {//Si es dopey, escanea durante 5 segundos y si es una roca pasa a collect, sino a scan
            secScan = secScan + 1 * Time.deltaTime;
            if (secScan >= 5f)
            {

                if (Physics.Raycast(ray, out toca, 5f))
                {

                    if (toca.transform.tag == ("roca"))
                    {

                        Debug.Log("Detecta roca");
                        animator.SetBool("collect", true);
                    }
                    else
                    {
                        animator.SetBool("scan", false);
                    }
                }
            }
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al salir se resetean los valores y scan pasa a false en caso de stun
        secScan = 0;
        //En caso de que le stuneen
        animator.SetBool("scan", false);
    }

   
   
}
