using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIDirector : MonoBehaviour
{
    public GameObject Storm;

    public float intervaloTormenta;
    public float duracionTormenta;
    public bool tormentona;


    public static AIDirector Instance { get; private set; }
    public GameObject[] barajas;
    public GameObject[] rover;
    public Transform[] destinoBarajas;
    public Transform[] destinoBarajasSinRepeticion;

    private NavMeshAgent agent;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Opcional
        DontDestroyOnLoad(gameObject);

    }


    public void Start()
    {
        


        tormentona = true;
        intervaloTormenta = Random.Range(25f, 40f);
        duracionTormenta = Random.Range(15f, 30f);


        barajas = GameObject.FindGameObjectsWithTag("waypoint");
        rover = GameObject.FindGameObjectsWithTag("Rover");
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
        
        tormentona = false;
        yield return new WaitForSeconds(20f);
        for (int i = 0; i < rover.Length; i++)
        {

            rover[i].transform.GetComponent<Animator>().SetBool("alarm", true);

        }
          
        yield return new WaitForSeconds(intervaloTormenta);
        StartStorm();
        for (int i = 0; i < rover.Length; i++)
        {

            rover[i].transform.GetComponent<Animator>().SetBool("waiting", true);
            rover[i].transform.GetComponent<Animator>().GetBehaviour<waiting>().vaciarInventario();

        }
        yield return new WaitForSeconds(duracionTormenta);
        StopStorm();
        for (int i = 0; i < rover.Length; i++)
        {

            rover[i].transform.GetComponent<Animator>().SetBool("waiting", false);

        }
        tormentona = true;
    }

    public Transform[] waypointsBarajas()
    {
        for (int i = 0; i < 6; i++)
        {

            destinoBarajasSinRepeticion = destinoBarajas.Distinct().ToArray();
            int waypoint = Random.Range(0, barajas.Length);
            destinoBarajas[i] = barajas[waypoint].transform;


            /*if(destinoBarajasSinRepeticion.Length < 6)
            {
                
                Debug.Log("if while");
                waypoint = Random.Range(0, barajas.Length);
                destinoBarajas[i] = barajas[waypoint].transform;
                destinoBarajasSinRepeticion = destinoBarajas.Distinct().ToArray();
                break;
            } */




            Debug.Log(i);


        }

        return destinoBarajasSinRepeticion;


    }
}
