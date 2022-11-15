using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }
    public GameObject[] barajas;
    public GameObject[] Rover;
    public Transform[] destinoBarajas;
    public Transform[] destinoBarajasSinRepeticion;

    public void Awake()
    {
        if(Instance == null)
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
    void Start()
    {
        
        barajas = GameObject.FindGameObjectsWithTag("waypoint");
        Rover = GameObject.FindGameObjectsWithTag("Rover");






        //Debug.Log(barajas[1].transform.position);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform[] waypointsBarajas()
    {
        for (int i = 0; i < 6; i++)
        {

            //destinoBarajasSinRepeticion = destinoBarajas.Distinct().ToArray();
            int waypoint = Random.Range(0, barajas.Length);
            destinoBarajas[i] = barajas[waypoint].transform;
            

            

        }
        
        return destinoBarajas;
       
    }
}
