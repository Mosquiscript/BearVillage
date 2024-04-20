using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMood : MonoBehaviour
{
    //Variables
    [SerializeField] private float moodInitial;
    [SerializeField] private float moodMax;
    //Propiedades
    public float Mood { get; private set; }
    private PlayerLife _playerLife;
    //Metodos
    private void Awake()
    {
        _playerLife = GetComponent<PlayerLife>();
    }
    private void Start()
    {
        Mood = moodInitial;
        UpdateMoodBar();
        
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            RestoreMood(10f);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            LowerMood(10f);
        }
    }
    //Restaurar energia del personaje solo si no esta muerto
    public void RestoreMood(float amount)
    {
        if (_playerLife.IsDead)
        {
            return;
        }
        Mood += amount;
        if (Mood > moodMax)
        {
            Mood = moodMax;
        }
        //Actualizamos la barra de energia del personaje seleccionado
        UpdateMoodBar(); 
    }
    //Bajar la energia del personaje
    public void LowerMood(float amount)
    {
        if (Mood >= amount)
        {
            Mood -= amount;
            UpdateMoodBar();
        }
    }
    //Restaurar toda la energia del personaje
    public void RestoreMood()
    {
        Mood = moodInitial;
        UpdateMoodBar();
    }
    //Actualizar la barra de eneria del personaje en la UI, llamamos el manager User Interface para que haga el trabajo
    private void UpdateMoodBar()
    {
        UIManager.Instance.UpdateMoodPlayer(Mood, moodMax);
    }
}
