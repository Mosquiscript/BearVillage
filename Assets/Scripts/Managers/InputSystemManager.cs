using System;
using UnityEngine;
using UnityEngine.InputSystem;


[DefaultExecutionOrder(-1)]
public class InputSystemManager : SingletonPattern<InputSystemManager>
{




    private CameraControlActions cameraControls;
    private int aux = 0;

    private void Awake()
    {
        cameraControls = new CameraControlActions();
    }

    private void OnEnable()
    {
        cameraControls.Enable();
    }
    private void OnDisable()
    {
        cameraControls.Disable();
    }
    private void Start()
    {
        
       
    }
    private void Update()
    {
        if (aux == 90)
        {
            //Debug.Log("Delta up" + cameraControls.Touch.MovementCamera.ReadValue<Vector2>());
            aux = 0;
        }
        else
            aux++;
        
    }




   

   
}
