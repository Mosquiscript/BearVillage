using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static ResourceManager Instance { get; private set; }

   
    public event EventHandler OnResourceAmountChanged;


    [SerializeField] private List<ResourceAmount> startingResourceAmountList;

     [SerializeField] private Dictionary<ResourceTypeSO, int> inventoryResourceTypeDic;


    private void Awake() {
        Instance = this;

        inventoryResourceTypeDic = new Dictionary<ResourceTypeSO, int>();
    }

    private void Start() {
        //Agregamos al diccionario los recursos que hay en el juego
        foreach (ResourceTypeSO resourceTypeSO in BearVillageAssets.Instance.resourceTypeArray) {
        
            inventoryResourceTypeDic[resourceTypeSO] = 0;
        }

       

        foreach (ResourceAmount resourceAmount in startingResourceAmountList) {
            AddResourceAmount(resourceAmount.resourceTypeSO, resourceAmount.amount);
        }
    }

    public void AddResourceAmount(List<ResourceTypeSO> inventoryResourceTypeList) {
        foreach (ResourceTypeSO resourceTypeSO in inventoryResourceTypeList) {
            inventoryResourceTypeDic[resourceTypeSO]++;
        }

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddResourceAmount(ResourceTypeSO resourceTypeSO, int amount) {
        inventoryResourceTypeDic[resourceTypeSO] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceTypeSO) {
        /* Debug.Log(inventoryResourceTypeDic[resourceTypeSO]); */
        return inventoryResourceTypeDic[resourceTypeSO];
    }

    public bool TrySpendResourceAmount(List<ResourceAmount> resourceAmountCostList) {
        bool canAfford = true;

        foreach (ResourceAmount resourceAmount in resourceAmountCostList) {
            if (GetResourceAmount(resourceAmount.resourceTypeSO) >= resourceAmount.amount) {    
                // Can afford this one
            } else {
                // Cannot afford
                canAfford = false;
                break;
            }
        }

        if (canAfford) {
            // Spend Resources
            foreach (ResourceAmount resourceAmount in resourceAmountCostList) {
                inventoryResourceTypeDic[resourceAmount.resourceTypeSO] -= resourceAmount.amount;
            }

            OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        } else {
            // Cannot afford
            return false;
        }
    }

}
