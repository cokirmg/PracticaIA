using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Geiser : MonoBehaviour
{
    public float MinRate = 5f;
    public float MaxRate = 15f;
    public ParticleSystem Vapor;
    public NavMeshObstacle obstacle;

    private void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        StartCoroutine(GeiserEruption());
    }

    private IEnumerator GeiserEruption()
    {
        ParticleSystem.EmissionModule emission = Vapor.emission;
        while (true)
        {
            emission.enabled = false;
            //Cuando el geiser está desactivado que se desactive el componente de obstáculo
            obstacle.enabled = false;
            yield return new WaitForSeconds(Random.Range(MinRate, MaxRate));
            emission.enabled = true;
            yield return new WaitForSeconds(1);
            //Cuando el geiser esté activado que se active el componente de obstáculo
            obstacle.enabled = true; ;
            yield return new WaitForSeconds(Random.Range(MinRate, MaxRate));
        }
    }
}
