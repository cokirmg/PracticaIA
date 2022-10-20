using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chargeCooldown : MonoBehaviour
{
    public bool charging;
    public bool needCharge;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        charging = false;
        needCharge = false;
        StartCoroutine(needChargeAgent());
    }

    // Update is called once per frame
    void Update()
    {
        
        if(needCharge == true)
        {
            StartCoroutine(ChargingAgent());

        }
    }
    public IEnumerator ChargingAgent()
        {

        agent.speed = 0f;
        yield return new WaitForSeconds(10f);
        agent.speed = 3.5f;
        
        StartCoroutine(needChargeAgent());
    }
    public IEnumerator needChargeAgent()
    {
        needCharge = false;
        yield return new WaitForSeconds(30f);
        needCharge = true;
    }
}
