using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Can be Damaged
 * */
public interface IUnitDamageable {

    bool IsDead();
    Vector3 GetPosition();
    void Damage(int damageAmount);
    Transform GetTransform();
    float GetAttackDistanceOffset();

}
