using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }
    public GameObject[] barajas;
    public GameObject[] Rover;

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






        Debug.Log(barajas[1].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
