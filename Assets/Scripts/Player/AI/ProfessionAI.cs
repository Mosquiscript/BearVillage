using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessionAI : MonoBehaviour
{
    private Player player;
    private PlayerController playerController;


    private void Awake() {
        player = GetComponent<Player>();
        playerController = GetComponent<PlayerController>();
        
    }
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    // Update is called once per frame
    private void Update()
    {
        StatePlayerMovement(player.StatePlayer);
    }

    public void StatePlayerMovement(State statePlayer){

        switch (statePlayer)
        {
            case State.Idle:
                /* Debug.Log("Parado"); */
                player.StatePlayer = State.MovingToTargePosition;
            break;
            case State.MovingToTargePosition:
                /* Debug.Log("Moviendo a un recolectar recurso"); */
                playerController.MoveTo(GameHandler.Instance.GetTransformMineNode().position, 0.5f);
                if (playerController.arrivedAtPosition)
                {
                    player.StatePlayer = State.GathererResourceNode;
                }
            break;
            case State.GathererResourceNode:
                /* Debug.Log("Recolectando recurso"); */
                player.StatePlayer = State.MovingToStorage;
            break;
            case State.MovingToStorage:
                /* Debug.Log("Moviendo a al Almacen"); */
                playerController.MoveTo(GameHandler.Instance.GetTransformStorage().position, 0.5f);
                if (playerController.arrivedAtPosition)
                {
                    player.StatePlayer = State.Idle;
                }
            break;
            default:
                /* Debug.Log("Ningun estado"); */
            break;
        }

    }

   
}
