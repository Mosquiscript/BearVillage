using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Tiene que tener el componente NavMeshAgent sino no funciona
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    RaycastHit hit;
    Ray ray;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
           Debug.Log("Requiere que se instale el paquete NavMeshAgent para funcionar correctamente");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //Movimiento de personaje 
        PlayerMovementPC();
        PlayerMovementAndroid();
    }
    //Se se da click derecho en el raton de la PC nuestro personaje se movera
    private void PlayerMovementPC()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
    //Si se da 1 touch(Android) en el mapa el personaje se movera en la posicion seleccionada
    private void PlayerMovementAndroid()
    {
        if (Input.touchCount == 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
