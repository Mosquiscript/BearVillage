using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Tiene que tener el componente NavMeshAgent sino no funciona
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public Vector3 targetPosition;
   
    public bool arrivedAtPosition;
    private NavMeshAgent agent;
    RaycastHit hit;
    Ray ray;

    private Player player;
    private void Awake()
    {
        arrivedAtPosition = false;
        player = GetComponent<Player>();
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
        /*  HandleMovement(); */
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

    /* public void MoveTo(Transform transformResource){
        agent.SetDestination(transformResource.position);
    } */

     public void SetTargetPosition(Vector3 targetPosition) {
        targetPosition.y = 0f;
        this.targetPosition = targetPosition;
    }

    public void MoveTo(Vector3 position, float stopDistance) {
        SetTargetPosition(position);
        
        //Si el personaje no a llegado al destino
        if (Vector3.Distance(transform.position, targetPosition) > stopDistance) 
        {
            float distanceBefore = Vector3.Distance(transform.position, targetPosition);
            Debug.Log("No a llegado al destino");
            arrivedAtPosition = false;
            agent.SetDestination(targetPosition);
        } else {
            // Llego al destino
            Debug.Log("Llego el destino");
            arrivedAtPosition = true;
        }
    }

    

   


    
}
