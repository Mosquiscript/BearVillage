using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLife : MonoBehaviour
{
    //Variables
    [SerializeField] protected float healthInitial;
    [SerializeField] protected float healthMax;
    //Propiedades
    public float Health { get; protected set; }
    public bool IsDead {get; protected set;}
    //Metodos
    protected virtual void Start()
    {
        Health = healthInitial;
    }
    //Si el personaje resive daño bajamos la barra de vida
    public void TakeDemage(float amount)
    {
        if (amount <= 0)
        {
            return;
        }
        if (Health > 0f)
        {
            Health -= amount;
            UpdateLifeBar(Health, healthMax);
            if (Health <= 0f)
            {
                UpdateLifeBar(Health, healthMax);
                CharacterIsDead();
            }
        }
    }
    protected virtual void UpdateLifeBar(float lifeCurrent, float healthMax)
    {
    }
    protected virtual void CharacterIsDead()
    {
    }
}
