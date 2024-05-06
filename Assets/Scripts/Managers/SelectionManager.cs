using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectionManager : MonoBehaviour
{
    public static Action<EnemyInteraction> EventEnemySelected;
    public static Action EventEnemyObjectNoSelected;

    public static Action<PlayerInteraction> EventPlayerSelected;
    public static Action EventPlayerObjectNoSelected;
    
    public PlayerInteraction playerSelected { get; set; }
    public EnemyInteraction enemySelected { get; set; }
    RaycastHit hit;
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectCharacter();
    }
    //Selecion de los personajes
    private void SelectCharacter()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Player")))
            {
               if (playerSelected == hit.collider.GetComponent<PlayerInteraction>())
               {    
                    Debug.Log("es igual");
                    return;
               }
                playerSelected = hit.collider.GetComponent<PlayerInteraction>();
                EventPlayerSelected?.Invoke(playerSelected);
            }else if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
            {
                enemySelected = hit.collider.GetComponent<EnemyInteraction>();
                EventEnemySelected?.Invoke(enemySelected);
            }
            else
            {
                if (playerSelected != null)
                {
                    EventPlayerObjectNoSelected?.Invoke();
                }
                EventEnemyObjectNoSelected?.Invoke();
            }
          
        }
    }


}
