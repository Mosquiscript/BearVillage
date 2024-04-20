using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Instancias
    public static UIManager Instance;
    //Variables
    [Header("Barra de vida del personaje")]
    [SerializeField] private Image healthPlayer;
    [Header("Barra de Energia del personaje")]
    [SerializeField] private Image energyPlayer;
    [Header("Barra de animo del personaje")]
    [SerializeField] private Image moodPlayer;
    [Header("Texto de la vida del personaje")]
    [SerializeField] private TextMeshProUGUI healthTMP;
    [Header("Texto de la energia del personaje")]
    [SerializeField] private TextMeshProUGUI energyTMP;
    [Header("Texto de la animo del personaje")]
    [SerializeField] private TextMeshProUGUI moodTMP;
    private float healthCurrent;
    private float healthMax;
    private float energyCurrent;
    private float energyMax;
    private float moodCurrent;
    private float moodMax;
    //Metodos
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        UpdateUIPlayer();
    }
    private void UpdateUIPlayer()
    {
        healthPlayer.fillAmount = Mathf.Lerp(healthPlayer.fillAmount, 
        healthCurrent / healthMax, 10f * Time.deltaTime);
        energyPlayer.fillAmount = Mathf.Lerp(energyPlayer.fillAmount, 
        energyCurrent / energyMax, 10f * Time.deltaTime);
        moodPlayer.fillAmount = Mathf.Lerp(moodPlayer.fillAmount, 
        moodCurrent / moodMax, 10f * Time.deltaTime);
        moodTMP.text = $"{moodCurrent}/{moodMax}";
        healthTMP.text = $"{healthCurrent}/{healthMax}";
        energyTMP.text = $"{energyCurrent}/{energyMax}";
    }
    public void UpdateLifePlayer(float phealthCurrent, float phealthMax)
    {
        healthCurrent = phealthCurrent;
        healthMax = phealthMax;
    }
    public void UpdateEnergyPlayer(float penergyCurrent, float penergythMax)
    {
        energyCurrent = penergyCurrent;
        energyMax = penergythMax;
    }
    public void UpdateMoodPlayer(float pmoodCurrent, float pmoodthMax)
    {
        moodCurrent = pmoodCurrent;
        moodMax = pmoodthMax;
    }
}
