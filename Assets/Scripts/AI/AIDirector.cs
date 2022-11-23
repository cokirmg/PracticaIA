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


    public static AIDirector Instance { get; private set; } //Patron Singleton
    public List<GameObject> barajasList = new List<GameObject>();
    public GameObject[] rover;
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

        //Busco todos los waypoints y los añado a la lista
        GameObject.FindGameObjectsWithTag("waypoint");
        barajasList.AddRange(GameObject.FindGameObjectsWithTag("waypoint"));
    }


    public void Start()
    {



        tormentona = true; //Se activa el periodo de tormenta
        intervaloTormenta = Random.Range(25f, 40f); //Espera de la tormenta
        duracionTormenta = Random.Range(15f, 30f);//Duración de la tormenta

        //Lista todos los rovers
        rover = GameObject.FindGameObjectsWithTag("Rover");
    }

    public void Update()
    {

        //El spawn de la tormenta
        if (tormentona)
        {
            StartCoroutine(Stormy());
        }
    }
    private void StartStorm()
    {
        Storm.SetActive(true); //Se activa la tormenta
    }

    private void StopStorm()
    {
        Storm.SetActive(false);//Se desactiva la tormenta
    }

    IEnumerator Stormy()
    {

        tormentona = false; //Se desactiva el booleano para que no se haga de nuevo
        yield return new WaitForSeconds(20f);//Empiezan los primeros 20 segundos hasta que empieza la tormenta
        for (int i = 0; i < rover.Length; i++)
        {

            rover[i].transform.GetComponent<Animator>().SetBool("alarm", true); //El director avisa a todos los rovers para que pasen a alarm

        }

        yield return new WaitForSeconds(intervaloTormenta);//Espera de la tormenta
        StartStorm();//Empieza la tormenta
        for (int i = 0; i < rover.Length; i++)
        {

            rover[i].transform.GetComponent<Animator>().SetBool("waiting", true); //Los que no hayan llegado a base que pasen a waiting
            rover[i].transform.GetComponent<Animator>().GetBehaviour<waiting>().vaciarInventario(); //Se aplica el metodo de vacio de inventario, que a los que estén en base no les afectará

        }
        yield return new WaitForSeconds(duracionTormenta);
        StopStorm();//Acaba la tormenta
        for (int i = 0; i < rover.Length; i++)
        {

            rover[i].transform.GetComponent<Animator>().SetBool("waiting", false); //Cuando acaba la tormenta todos los estados de los rovers de waiting pasan a false

        }
        tormentona = true;
    }

    public Transform[] waypointsBarajas()
    {
        Transform[] destinoBarajas = new Transform[6]; 
        List<GameObject> barajas = new(); //Crea una nueva lista para saber si se repiten los puntos
        barajas.AddRange(barajasList); //Añade todos los valores a la lista
        for (int i = 0; i < destinoBarajas.Length; ++i)
        {
            int indice = Random.Range(0, barajas.Count);
            destinoBarajas[i] = barajas[indice].transform; //Coge un valor aleatorio de la lista y lo borra de la que están todos
            barajas.RemoveAt(indice);
        }

        return destinoBarajas; //Le devuelve la lista entera
    }
}
