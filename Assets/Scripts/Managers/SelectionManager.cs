using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectionManager : MonoBehaviour
{
    public static Action<GameObject> EventObjectSelected;
    public static Action EventObjectNoSelected;
    public GameObject ObjectSelected { get; set; }
    RaycastHit hit;
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        SelectObject();
    }
    //Cuando demos click a un objeto se seleccionara si tiene el layer de "Selectable"
    private void SelectObject()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Selectable")))
            {
                ObjectSelected = hit.collider.gameObject;
                EventObjectSelected?.Invoke(ObjectSelected);
            }
            else
            {
                EventObjectNoSelected?.Invoke();
            }
        }
    }
}
