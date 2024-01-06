using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
//using UnityEngine.CoreModule

public class InputManager : MonoBehaviour
{
    [Header("NA")]
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;
    [Header("Seleccion del plano en el que nos moveremos")]
    [SerializeField]
    private LayerMask placementLayermask;

    public Vector3 GetSelectedMapPosition() { 
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane; //Objetos de la camara que se renderizar, osea en el plano cercano
        Ray ray = sceneCamera.ScreenPointToRay(mousePos); //General un ray
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
