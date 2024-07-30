using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionUnitBehaviour : MonoBehaviour, IUnitBehaviour {

    public event EventHandler OnStartConstructing;
    


    private enum State {
        GoingToBuilding,
        Constructing,
    }

    private PlayerController unit;
    private State state;
    private BuildingConstruction buildingConstruction;
    private float constructionTimer;

    private void Awake() {
        unit = GetComponent<PlayerController>();
    }

    public void SetBuildingConstruction(BuildingConstruction buildingConstruction) {
        unit.SetActiveBehaviour(this);
        this.buildingConstruction = buildingConstruction;
        state = State.GoingToBuilding;
    }

    public void UpdateBehaviour() {
        switch (state) {
            case State.GoingToBuilding:
                unit.SetDestination(buildingConstruction.GetPosition());

               /*  if (Vector3.Distance(unit.GetPosition(), buildingConstruction.GetPosition()) < buildingConstruction.GetConstructionDistanceOffset()) {
                    // Reached!
                    unit.StopMoving();
                    // Start Constructing
                    state = State.Constructing;

                    OnStartConstructing?.Invoke(this, EventArgs.Empty);
                } */
                break;
            case State.Constructing:
                constructionTimer -= Time.deltaTime;
                if (constructionTimer < 0) {
                    float constructionTimerMax = 1f;
                    constructionTimer += constructionTimerMax;

                    buildingConstruction.AddProgress(1f);

                    /* if (buildingConstruction.IsConstructed()) {
                        unit.NormalMoveTo(unit.GetPosition());
                    } */
                }
                break;
        }
    }

    

}
