using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

    private static List<Storage> instanceList;
    [SerializeField] private Transform placeToCollectResource; 
    private void Awake() {
        if (placeToCollectResource == null)
        {
            Debug.LogError("El lugar para recoger los recursos no esta referenciado");
        }

        if (instanceList == null) {
            instanceList = new List<Storage>();
        }

        instanceList.Add(this);
    }



    public static Storage GetClosestStorage(Vector3 position) {
        if (instanceList == null) return null; // No storage exists!

        Storage closest = null;
        foreach (Storage storage in instanceList) {
            if (closest == null) {
                closest = storage;
            } else {
                if (Vector3.Distance(position, storage.GetPosition()) < Vector3.Distance(position, closest.GetPosition())) {
                    closest = storage;
                }
            }
        }
        return closest;
    }


    public Vector3 GetPosition() {
        return placeToCollectResource.position;
    }

}