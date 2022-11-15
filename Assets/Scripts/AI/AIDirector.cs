using System.Collections;
using UnityEngine;

public class AIDirector : MonoBehaviour
{
    public GameObject Storm;

    public float intervaloTormenta;
    public float duracionTormenta;
    public bool tormentona;

    public void Start()
    {
        tormentona = true;
        intervaloTormenta = Random.Range(45f, 60f);
        duracionTormenta = Random.Range(15f, 30f);
    }

    public void Update()
    {
        if(tormentona)
        {
            StartCoroutine(Stormy());
        }
    }
    private void StartStorm()
    {
        Storm.SetActive(true);
    }

    private void StopStorm()
    {
        Storm.SetActive(false);
    }
     
    IEnumerator Stormy()
    {
        //Varios yield return, distinc si l lenght es menor que 6
        tormentona = false;
        yield return new WaitForSeconds(intervaloTormenta);
        StartStorm();
        yield return new WaitForSeconds(duracionTormenta);
        StopStorm();
        tormentona = true;
    }
}
