using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Requiere estos componentes para que funcione
[RequireComponent(typeof(PlayerLife))]
[RequireComponent(typeof(PlayerEnergy))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerMood))]
[RequireComponent(typeof(PlayerInteraction))]

public class Player : MonoBehaviour
{
    //Propiedades
    public State StatePlayer {get; set; }

    private void Awake() {
        StatePlayer = State.Idle;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   
    
}
