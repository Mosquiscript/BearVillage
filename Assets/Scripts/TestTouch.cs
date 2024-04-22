using System;
using UnityEngine;


public class TestTouch : MonoBehaviour
{
    private InputSystemManager inputSystemManager;
    [SerializeField]
    private Camera cameraMain;

    private void Awake()
    {
        inputSystemManager = InputSystemManager.Instance;
        //cameraMain = Camera.;
        Debug.Log(cameraMain);
    }
    private void OnEnable()
    {
        inputSystemManager.OnStartTouch += Move;
        Debug.Log("se suscribe");
    }
    private void OnDisable()
    {
        inputSystemManager.OnEndTouch -= Move;
    }

    public void Move(Vector2 screenPosition, float time)
    {

        Debug.Log("Las coordenadas " + screenPosition + " "+ time);
        Debug.Log(cameraMain.nearClipPlane);
        Vector3 screenCoordinates = new Vector3(screenPosition.x,cameraMain.nearClipPlane,screenPosition.y);
        Debug.Log(screenCoordinates);
        Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.y = 1;
        transform.position = worldCoordinates;
    }

    /*public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane; //Objetos de la camara que se renderizar, osea en el plano cercano
        Ray ray = sceneCamera.ScreenPointToRay(mousePos); //General un ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }*/
}
