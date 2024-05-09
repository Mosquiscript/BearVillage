using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Estado del personaje
public enum Profession
{
    Blacksmith,
    Miner,
    Woodcutter,
    Builder,
    Warrior,
    Archer
}
//Estado del personaje
public enum State
{
    Idle,
    MovingToTargePosition,
    GathererResourceNode,
    MovingToStorage,
    Mining,
    CreatingTools,
    Fellingtree,
    Building,
    Attacking
}
//Requiere estos componentes para que funcione
[RequireComponent(typeof(PlayerLife))]
[RequireComponent(typeof(PlayerEnergy))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerMood))]
[RequireComponent(typeof(PlayerInteraction))]
[RequireComponent(typeof(PlayerAttack))]

public class Player : MonoBehaviour
{
    //Propiedades
    public State StatePlayer {get; set; }

    private void Awake() {
        /* StatePlayer = State.MovingToResourceNode; */
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
