using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearVillageAssets : MonoBehaviour {

    public static BearVillageAssets Instance { get; private set; }


    private void Awake() {
        Instance = this;
    }




    public Transform pfUnitProjectile;
    public Transform pfUnitEnemy;
    public Transform pfBuildingConstruction;


   /*  public BuildingTypeSO_Refs buildingTypeSO_Refs; */

    /* [Serializable] */
    /* public class BuildingTypeSO_Refs {

        public BuildingTypeSO none;
        public BuildingTypeSO storage;
        public BuildingTypeSO barracks;

    } */




    public ResourceNodeSO_Refs resourceNodeSO_Refs;

    [Serializable]
    public class ResourceNodeSO_Refs {

        public ResourceNodeSO stone;
        public ResourceNodeSO wood;

    } 


    public ResourceTypeSO_Refs resourceTypeSO_Refs;
    public ResourceTypeSO[] resourceTypeArray;

    [Serializable]
    public class ResourceTypeSO_Refs {

        public ResourceTypeSO stone;
        public ResourceTypeSO wood;
        public ResourceTypeSO iron;

    }


 /*    public UnitTypeSO_Refs unitTypeSO_Refs;

    [Serializable]
    public class UnitTypeSO_Refs {

        public UnitTypeSO villager;
        public UnitTypeSO melee;
        public UnitTypeSO ranged;

        public List<UnitTypeSO> GetAll() {
            return new List<UnitTypeSO> { 
                villager, 
                melee, 
                ranged
            };
        }

    } */

}
