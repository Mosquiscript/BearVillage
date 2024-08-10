using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _lastMousePosition;

    [SerializeField] private Camera _camera;
    // Start is called before the first frame update

    private void Awake() {
        _camera = Camera.main;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementInput();
    }

    private void CheckMovementInput()
    {
      if (Input.GetMouseButtonDown(0))
      {
            _lastMousePosition = Input.mousePosition;
      }


      if (!Input.GetMouseButton(0))
      {
            return;
      }

      Vector3 mouseWorldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
      Vector3 lastMousePoint = _camera.ScreenToWorldPoint(_lastMousePosition);  

      Debug.Log("mouseWorldPoint: " +mouseWorldPoint); 
      Debug.Log("lastMousePoint: " +lastMousePoint);     

      Vector3 delta = mouseWorldPoint - lastMousePoint;

      _lastMousePosition = Input.mousePosition;
       
        _camera.transform.position += -delta;


    }
}
