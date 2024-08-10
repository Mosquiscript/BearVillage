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
    [Header("Consoles Log")]
    [SerializeField] private TextMeshProUGUI ConsoleLog1;
    
    //Variables
    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelStore;
    [SerializeField] private GameObject panelBarBuildings;
    [SerializeField] private GameObject panelInfoPlayer;
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
    [Header("Estadisticas Personaje")]
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private TextMeshProUGUI ganderTMP;
    [SerializeField] private TextMeshProUGUI professionTMP;
    [SerializeField] private TextMeshProUGUI ageTMP;
    [SerializeField] private TextMeshProUGUI liveTMP;
    [SerializeField] private TextMeshProUGUI energyPanelTMP;
    [SerializeField] private TextMeshProUGUI moodPanelTMP;
    [SerializeField] private TextMeshProUGUI levelProfessionTMP;
    [Header("Recursos")]
    [SerializeField] private TextMeshProUGUI mineTMP;
    [SerializeField] private TextMeshProUGUI foodTMP;
    [SerializeField] private TextMeshProUGUI woodTMP;
    private float healthCurrent;
    private float healthMax;
    private float energyCurrent;
    private float energyMax;
    private float moodCurrent;
    private float moodMax;


  private TextMeshProUGUI resourceAmountText;
    private Dictionary<ResourceTypeSO, TextMeshProUGUI> resourceTextDic;



    //Metodos
    void Awake()
    {
        Instance = this;
        resourceTextDic = new Dictionary<ResourceTypeSO, TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
        resourceTextDic[BearVillageAssets.Instance.resourceTypeSO_Refs.stone] = woodTMP;
        resourceTextDic[BearVillageAssets.Instance.resourceTypeSO_Refs.wood] = foodTMP;
        resourceTextDic[BearVillageAssets.Instance.resourceTypeSO_Refs.iron] = mineTMP;

       
        ResourceManager.Instance.OnResourceAmountChanged += Instance_OnResourceAmountChanged;

        UpdateResourceAmounts();    
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
    public void ShowPanelStats()
    {
        if (panelStats.activeSelf == false)
        {
            panelStats.SetActive(true);
        }else{
            panelStats.SetActive(false);
        }
    }
    public void ShowPanelStore()
    {
        if (panelStore.activeSelf == false)
        {
            panelStore.SetActive(true);
        }else{
            panelStore.SetActive(false);    
        }
    }
    public void ShowPanelBarBuildings()
    {
        if (panelBarBuildings.activeSelf == false)
        {
            panelBarBuildings.SetActive(true);
        }else{
            panelBarBuildings.SetActive(false);    
        }
    }
    public void ShowPanelInfoPlayer(bool state)
    {
       panelInfoPlayer.SetActive(state);
    }

    private void Instance_OnResourceAmountChanged(object sender, System.EventArgs e) {
        UpdateResourceAmounts();
    }

    private void UpdateResourceAmounts() {
        foreach (ResourceTypeSO resourceTypeSO in BearVillageAssets.Instance.resourceTypeArray) {
            resourceTextDic[resourceTypeSO].text = ResourceManager.Instance.GetResourceAmount(resourceTypeSO).ToString(); 
        }
    }

    public void ConsolesLogs(string text1)
    {
        ConsoleLog1.text = $"Console Log: {text1}"; 
    }
}
