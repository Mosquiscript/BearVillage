using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyUnitBehaviour : MonoBehaviour, IUnitBehaviour {

    private enum State {
        PatrollingMoving,
        PatrollingIdle,
        MoveToTarget,
        AttackingTarget,
    }

    [SerializeField] private bool patrol;
    [SerializeField] private int damageAmount;
    [SerializeField] private float attackTimerMax;
    [SerializeField] protected float attackRange;
    [SerializeField] private float findTargetDistance;
    
    protected PlayerController unit;
    private State state;
    protected PlayerController targetUnit;
    private float attackTimer;
    private Vector3 startPosition;
    private Vector3 patrolPosition;
    private float patrolTimer;

    public void Awake() {
        unit = GetComponent<PlayerController>();
        startPosition = transform.position;
        state = State.PatrollingIdle;
    }

    private void Start() {
        unit.SetActiveBehaviour(this);
    }

    public virtual void UpdateBehaviour() {
        switch (state) {
            case State.PatrollingMoving:
                if (patrol) {
                    unit.SetDestination(patrolPosition);

                    float reachedDistance = 2f;
                    if (Vector3.Distance(unit.GetPosition(), patrolPosition) < reachedDistance) {
                        state = State.PatrollingIdle;
                    }
                }

                LookForTarget();
                break;
            case State.PatrollingIdle:
                if (patrol) {
                    patrolTimer -= Time.deltaTime;
                    if (patrolTimer < 0f) {
                        patrolTimer = Random.Range(0f, 2f);
                        patrolPosition = startPosition + UtilsClass.GetRandomDirXZ() * Random.Range(0f, 10f);
                        state = State.PatrollingMoving;
                    }
                }

                LookForTarget();
                break;

            case State.MoveToTarget:
                if (targetUnit == null || targetUnit.IsDead()) {
                    targetUnit = null;
                    state = State.PatrollingMoving;
                    return;
                }

                unit.SetDestination(targetUnit.GetPosition());

                if (Vector3.Distance(unit.GetPosition(), targetUnit.GetPosition()) < GetAttackRange()) {
                    // Reached!
                    unit.StopMoving();
                    state = State.AttackingTarget;
                }
                break;

            case State.AttackingTarget:
                if (AttackingTarget()) {
                    if (targetUnit.IsDead()) {
                        targetUnit = null;
                        state = State.PatrollingMoving;
                    }
                }
                break;
        }
    }

    protected virtual float GetAttackRange() {
        return attackRange;
    }

    protected virtual bool AttackingTarget() {
        // Handle attacking of this target
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0) {
            attackTimer += attackTimerMax;

            /* targetUnit.Damage(damageAmount); */

            return true;
        }

        return false;
    }

    private void LookForTarget() {
        // Look for Target
        Collider[] colliderArray = Physics.OverlapSphere(unit.GetPosition(), findTargetDistance);
        foreach (Collider collider in colliderArray) {
            if (collider.TryGetComponent<PlayerController>(out PlayerController chopChopUnit)) {
                if (!chopChopUnit.IsEnemy()) {
                    // Not an Enemy, attack
                    targetUnit = chopChopUnit;
                    state = State.MoveToTarget;
                }
            }
        }
    }

}
