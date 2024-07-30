using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour {

    [SerializeField] private ResourceTypeSO resourceTypeSO;
    [SerializeField] private int resourceAmount;
    [SerializeField] private Transform placeToMine; 

    private void Awake() {
        if (resourceTypeSO == null)
        {
            Debug.Log("No esta referenciado el recurso tipo SO");
        }
        if (resourceAmount <= 0)
        {
            Debug.Log("No tenemos candidad que recoger de este recurso");
        }
        if (placeToMine == null)
        {
            Debug.LogError("El lugar para minar no esta referenciado");
        }
    }


    public Vector3 GetPosition() {
        return placeToMine.position;
    }

    public ResourceTypeSO GetResourceTypeSO() {
        return resourceTypeSO;
    }

    public void GrabResource() {
        resourceAmount--;

        if (resourceAmount <= 0) {
            Destroy(gameObject);
        }
    }

    public bool HasResources() {
        return resourceAmount > 0;
    }

}
