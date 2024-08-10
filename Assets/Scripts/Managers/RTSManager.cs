using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSManager : MonoBehaviour {

    [SerializeField] private UnitSelectionManager unitSelectionManager;
    [SerializeField] private InputSystemManager inputSystemManager;

    private Camera mainCamera;
    private Touch3D touch3D;

    private void Awake() {
        mainCamera = Camera.main;
        Application.targetFrameRate = 100;
        touch3D = Touch3D.Instance;
        inputSystemManager = InputSystemManager.Instance;
    }

    private void OnEnable()
    {
        //Cuando se suscribe en el evento nos manda 2 parametros que son Vector2 y el tiempo
        /* inputSystemManager.OnStartTouch += MoveToUnits; */
    }
    private void OnDisable()
    {
        /* inputSystemManager.OnEndTouch -= MoveToUnits; */
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveToUnits();
        }
        
    }


    private void MoveToUnits(/* Vector2 screenPosition, float time */)
    {
        
            // Click derecho del raton
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit)) 
            {

                //Esta linea es para nuestro personajes se muevan normal
                Action<PlayerController> unitAction = (PlayerController unit) => unit.NormalMoveTo(Mouse3D.GetMouseWorldPosition());

                //Orden para recolectar recursos
                if (raycastHit.collider.TryGetComponent<ResourceNode>(out ResourceNode resourceNode)) {
                    unitAction = (PlayerController unit) => {
                        if (unit.TryGetComponent<GatheringUnitBehaviour>(out GatheringUnitBehaviour gatheringUnitBehaviour)) {
                            gatheringUnitBehaviour.SetGatherResources(resourceNode);
                            
                        }
                    };
                }

                //Orden para construir los edificios
                if (raycastHit.collider.TryGetComponent(out BuildingConstruction buildingConstruction)) {
                    unitAction = (PlayerController unit) => {
                        if (unit.TryGetComponent<ConstructionUnitBehaviour>(out ConstructionUnitBehaviour constructionUnitBehaviour)) {
                            constructionUnitBehaviour.SetBuildingConstruction(buildingConstruction);
                        }
                    };
                }

                // Test Attack Enemy Order
              /*   if (raycastHit.collider.TryGetComponent<PlayerController>(out PlayerController targetPlayerController)) {
                    if (targetPlayerController.IsEnemy()) {
                        unitAction = (PlayerController unit) => {
                            if (unit.TryGetComponent<AttackingUnitBehaviour>(out AttackingUnitBehaviour attackingUnitBehaviour)) {
                                attackingUnitBehaviour.SetEnemyTarget(targetPlayerController);
                            }
                        };
                    }
                } */

                //Ejecuta las ordenes de los personajes seleccionados
                foreach (PlayerController playerController in unitSelectionManager.GetSelectedUnitList()) {
                    if (playerController.IsDead()) continue;
                    unitAction(playerController);
                }
            }
        
    }

}
