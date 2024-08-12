using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackingUnitBehaviour : AttackingUnitBehaviour {

    public event EventHandler OnRangedAttacking;

    private float rangedAttackTimer;

    public override void SetEnemyTarget(IUnitDamageable targetUnit) {
        base.SetEnemyTarget(targetUnit);
    }

    protected override bool AttackingTarget() {
        // Handle attacking of this target
        transform.LookAt(targetUnit.GetPosition());

        rangedAttackTimer -= Time.deltaTime;
        float attackTimerMax = .5f;
        if (rangedAttackTimer < 0) {
            rangedAttackTimer += attackTimerMax;

            OnRangedAttacking?.Invoke(this, EventArgs.Empty);

            UnitProjectile.Create(transform.position + Vector3.up * 1f, targetUnit, damageAmount);

            return true;
        }

        return false;
    }

}
