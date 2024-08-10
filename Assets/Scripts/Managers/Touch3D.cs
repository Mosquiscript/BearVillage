using System;
using UnityEngine;

public class Touch3D : SingletonPattern<Touch3D>
{
    public Transform objectoTestMove;
    [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();
    private InputSystemManager inputSystemManager;
    [SerializeField]
    private Camera cameraMain;
    private Vector3 worldTouchPosition;

    private void Awake()
    {
        inputSystemManager = InputSystemManager.Instance;
        cameraMain = Camera.main;
    }

    private void OnEnable()
    {
        //Cuando se suscribe en el evento nos manda 2 parametros que son Vector2 y el tiempo
        /* inputSystemManager.OnStartTouch += SetTouchWorldPosition; */
    }
    private void OnDisable()
    {
        /* inputSystemManager.OnEndTouch -= SetTouchWorldPosition; */
    }

    public void SetTouchWorldPosition(Vector2 screenPosition, float time)
    {

        /* Debug.Log("Las coordenadas " + screenPosition + " "+ time); */
        Vector3 screenCoordinates = new Vector3(screenPosition.x,screenPosition.y,0f);
       /*  Debug.Log(screenCoordinates);
        Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.y = 1  ; */
        Ray ray = Camera.main.ScreenPointToRay(screenCoordinates);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask)) {
                /* return raycastHit.point; */
                /* objectoTestMove.position = raycastHit.point; */
                worldTouchPosition = raycastHit.point;
            } 
        
    }

  
    public Vector3 GetTouchWorldPosition()
    {
        return worldTouchPosition;
    }



}
