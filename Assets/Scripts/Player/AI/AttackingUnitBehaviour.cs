using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingUnitBehaviour : MonoBehaviour, IUnitBehaviour {

    public event EventHandler OnAttacking;

    private enum State {
        MoveToTarget,
        AttackingTarget,
    }


    [SerializeField] protected int damageAmount;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackTimerMax;
    
    protected ChopChopUnit unit;
    private State state;
    protected IUnitDamageable targetUnit;
    private float attackTimer;

    public void Awake() {
        unit = GetComponent<ChopChopUnit>();
    }

    public virtual void SetEnemyTarget(IUnitDamageable targetUnit) {
        unit.SetActiveBehaviour(this);
        this.targetUnit = targetUnit;
        state = State.MoveToTarget;
    }

    public virtual void UpdateBehaviour() {
        if (targetUnit == null || targetUnit.IsDead()) {
            unit.SetStateNormal();
            return;
        }

        switch (state) {
            case State.MoveToTarget:
                unit.SetDestination(targetUnit.GetPosition());

                if (Vector3.Distance(unit.GetPosition(), targetUnit.GetPosition()) < GetAttackRange() + targetUnit.GetAttackDistanceOffset()) {
                    // Reached!
                    unit.StopMoving();
                    state = State.AttackingTarget;
                }
                break;

            case State.AttackingTarget:
                if (AttackingTarget()) {
                    if (targetUnit.IsDead()) {
                        unit.SetStateNormal();
                    } else {
                        state = State.MoveToTarget;
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
        transform.LookAt(targetUnit.GetPosition());

        attackTimer -= Time.deltaTime;
        if (attackTimer < 0) {
            attackTimer += attackTimerMax;

            OnAttacking?.Invoke(this, EventArgs.Empty);

            targetUnit.Damage(damageAmount);

            return true;
        }

        return false;
    }

    public bool IsActive() {
        return unit.GetActiveBehaviour() == this;
    }

}
