using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PatrolUnitBehaviour : MonoBehaviour, IUnitBehaviour {
    private enum State {
        PatrollingMoving,
        PatrollingIdle,
    }

    protected PlayerController unit;
    private State state;
    private Vector3 startPosition;
    private Vector3 patrolPosition;
    private float patrolTimer;

    public void Awake() {
        unit = GetComponent<PlayerController>();
        startPosition = transform.position;
        state = State.PatrollingIdle;
    }

    private void Start() {
        StartPatrolling();
    }

    public void StartPatrolling() {
        unit.SetActiveBehaviour(this);
    }

    public virtual void UpdateBehaviour() {
        switch (state) {
            case State.PatrollingMoving:
                unit.SetDestination(patrolPosition);

                float reachedDistance = 2f;
                if (Vector3.Distance(unit.GetPosition(), patrolPosition) < reachedDistance) {
                    state = State.PatrollingIdle;
                }
                break;
            case State.PatrollingIdle:
                patrolTimer -= Time.deltaTime;
                if (patrolTimer < 0f) {
                    patrolTimer = Random.Range(0f, 2f);
                    patrolPosition = startPosition + UtilsClass.GetRandomDirXZ() * Random.Range(0f, 10f);
                    state = State.PatrollingMoving;
                }
                break;
        }
    }

}
