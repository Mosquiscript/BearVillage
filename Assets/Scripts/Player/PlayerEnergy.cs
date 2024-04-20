using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    //Variables
    [SerializeField] private float energyInitial;
    [SerializeField] private float energyMax;
    //Propiedades
    public float Energy { get; private set; }
    private PlayerLife _playerLife;
    //Metodos
    private void Awake()
    {
        _playerLife = GetComponent<PlayerLife>();
    }
    private void Start()
    {
        Energy = energyInitial;
        UpdateEnergyBar();
        
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            RestoreEnergy(10f);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            LowerEnergy(10f);
        }
    }
    //Restaurar energia del personaje solo si no esta muerto
    public void RestoreEnergy(float amount)
    {
        if (_playerLife.IsDead)
        {
            return;
        }
        Energy += amount;
        if (Energy > energyMax)
        {
            Energy = energyMax;
        }
        //Actualizamos la barra de energia del personaje seleccionado
        UpdateEnergyBar(); 
    }
    //Bajar la energia del personaje
    public void LowerEnergy(float amount)
    {
        if (Energy >= amount)
        {
            Energy -= amount;
            UpdateEnergyBar();
        }
    }
    //Restaurar toda la energia del personaje
    public void RestoreEnergy()
    {
        Energy = energyInitial;
        UpdateEnergyBar();
    }
    //Actualizar la barra de eneria del personaje en la UI, llamamos el manager User Interface para que haga el trabajo
    private void UpdateEnergyBar()
    {
        UIManager.Instance.UpdateEnergyPlayer(Energy, energyMax);
    }
}
