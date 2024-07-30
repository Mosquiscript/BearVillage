using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalUnitBehaviour : MonoBehaviour, IUnitBehaviour {

    private PlayerController unit;

    private void Awake() {
        unit = GetComponent<PlayerController>();
    }

    public void UpdateBehaviour() {
        if (!unit.IsStopped()) {
            if (unit.GetNavMeshAgent().remainingDistance <= .5f) {
                unit.StopMoving();
            }
        }
    }

    public void MoveTo(Vector3 destinationPosition) {
        unit.SetActiveBehaviour(this);
        unit.SetDestination(destinationPosition);
    }

}
