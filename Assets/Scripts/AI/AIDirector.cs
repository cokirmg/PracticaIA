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
        

            //destinoBarajasSinRepeticion = destinoBarajas.Distinct().ToArray();
           
            //destinoBarajas[i] = barajas[waypoint].transform;


            /*if(destinoBarajasSinRepeticion.Length < 6)
            {
                
                Debug.Log("if while");
                waypoint = Random.Range(0, barajas.Length);
                destinoBarajas[i] = barajas[waypoint].transform;
                destinoBarajasSinRepeticion = destinoBarajas.Distinct().ToArray();
                break;
            } */
            int añadido = 0;
        Debug.Log("entra while");
            while (añadido < 6)
            {
                
                    int indice = Random.Range(0, barajas.Length);
                    Debug.Log(indice);
                Transform waypoint = barajas[indice].transform;
                    if (!existePunto(waypoint))
                        {
                            //int waypoint = Random.Range(0, barajas.Length);
                            destinoBarajas[añadido] = waypoint;
                            añadido++;
                        }

            }


            


        

        return destinoBarajas;


    }
    public bool existePunto(Transform waypoint)
    {
        bool existe = false;
        for (int i = 0; i < destinoBarajas.Length; i++)
        {
            if (destinoBarajas[i] == waypoint)
            {
                existe = true;
            }
            
        }
        return existe;
    }
}
