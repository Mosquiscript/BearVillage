using System;
using UnityEngine;

public class PlayerLife : BaseLife
{
    //Aciones
    public static Action EventoPersonajeDerrotado;
    //Propiedades
    public bool CanBeCured => Health < healthMax;
    //Metodos
    private void Awake()
    {
    }
    protected override void Start()
    {
        base.Start();
        UpdateLifeBar(Health, healthMax);
    }
    private void Update()
    {
        //Si precionamos la tecla T llamamos el metodo de recibir daño
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDemage(10);
        }
        //Si precionamos la tecla Y llamamos el metodo de restaurar vida
        if (Input.GetKeyDown(KeyCode.Y))
        {    
            RestoreHealth(10);           
        }
    }
    //Restaura la vida del Jugador, solo si no esta muerto
    public void RestoreHealth(float amount)
    {
        if (IsDead)
        {
            return;
        }
        if (CanBeCured)
        {
            Health += amount;
            if (Health > healthMax)
            {
                Health = healthMax;
            }
            //Actualizamos la barra de vida del personaje seleccionado
            UpdateLifeBar(Health, healthMax);
        }  
    }
    protected override void CharacterIsDead()
    {
    }
    public void RestoreCharacter()
    {
    }
    //Actualizar la barra del personaje en la user interface
    protected override void UpdateLifeBar(float pcurrentHealth, float phealthMax)
    {
        UIManager.Instance.UpdateLifePlayer(pcurrentHealth, phealthMax);
    }
}