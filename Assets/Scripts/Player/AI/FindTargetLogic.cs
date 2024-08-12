using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetLogic : MonoBehaviour {


    [SerializeField] private bool targetIsEnemy;

    private AttackingUnitBehaviour attackingUnitBehaviour;
    private float findTargetTimer;

    private void Awake() {
        attackingUnitBehaviour = gameObject.GetComponent<AttackingUnitBehaviour>();
    }

    private void Update() {
        if (attackingUnitBehaviour.IsActive()) {
            // Already attacking, don't look for target
            return;
        }

        findTargetTimer -= Time.deltaTime;
        if (findTargetTimer < 0f) {
            float findTargetTimerMax = .2f;
            findTargetTimer += findTargetTimerMax;
            FindTarget();
        }
    }

    private void FindTarget() {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider collider in colliderArray) {
            if (collider.TryGetComponent<ChopChopUnit>(out ChopChopUnit chopChopUnit)) {
                if (chopChopUnit.IsEnemy() == targetIsEnemy) {
                    // It's an Enemy Unit, attack
                    if (attackingUnitBehaviour != null) {
                        attackingUnitBehaviour.SetEnemyTarget(chopChopUnit);
                        return;
                    }
                }
            }
            if (collider.TryGetComponent<Building>(out Building building)) {
                if (building.IsEnemy() == targetIsEnemy) {
                    // It's an Enemy Building, attack
                    if (attackingUnitBehaviour != null) {
                        attackingUnitBehaviour.SetEnemyTarget(building);
                        return;
                    }
                }
            }
        }
    }

}
