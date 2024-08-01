using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSManager : MonoBehaviour {

    [SerializeField] private UnitSelectionManager unitSelectionManager;

    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;

        Application.targetFrameRate = 100;
    }

    private void Update() {
        // Test Orders
        if (Input.GetMouseButtonDown(1)) {
            // Right Mouse Button Click
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit)) {
                // Raycast hit something

                // Normal move action
                Action<PlayerController> unitAction = (PlayerController unit) => unit.NormalMoveTo(Mouse3D.GetMouseWorldPosition());

                // Test Resource Node Order
                if (raycastHit.collider.TryGetComponent<ResourceNode>(out ResourceNode resourceNode)) {
                    unitAction = (PlayerController unit) => {
                        if (unit.TryGetComponent<GatheringUnitBehaviour>(out GatheringUnitBehaviour gatheringUnitBehaviour)) {
                            gatheringUnitBehaviour.SetGatherResources(resourceNode);
                            
                        }
                    };
                }

                // Test Building Construction Order
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

                // Execute Action
                foreach (PlayerController playerController in unitSelectionManager.GetSelectedUnitList()) {
                    if (playerController.IsDead()) continue;
                    unitAction(playerController);
                }
            }
        }
    }

}
