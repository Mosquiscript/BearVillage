using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    private Transform resourceTransform;

    private int resourceAmount;

    public Resource(Transform resourceTransform){
        this.resourceTransform = resourceTransform;
        resourceAmount = 3;
    }
    //Optener la posicion del recurso
    public Vector3 GetPosition()
    {
        return resourceTransform.position;
    }
    //Tomar recursos
    public void GrabResource(){
        resourceAmount -= 1;
        if (resourceAmount <= 0 )
        {
            Debug.Log("Se termino la mina hay que hacer algo si destruirla o algo");
        }
        Debug.Log("Se rebajo del recurso solo queda: " + resourceAmount);
    }
    //Si hay recursos
    public bool HasResource(){
        return resourceAmount > 0;
    }
}
