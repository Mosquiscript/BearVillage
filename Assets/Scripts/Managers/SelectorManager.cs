using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    //public static Action<EnemigoInteraccion> EventoEnemigoSeleccionado;
    public static Action EventoObjetoNoSeleccionado;
    //public EnemigoInteraccion EnemigoSeleccionado { get; set; }
    //public static Action<PersonajeInteracion> EventoPersonajeSeleccionado;
    public static Action EventoPersonajeNoSeleccionado;
    //public PersonajeInteracion PersonajeSeleccionado { get; set; }
    public LayerMask layerMaskPersonaje;
    public LayerMask layerMaskEnemigo;
    float longitudInfinita = Mathf.Infinity;
    RaycastHit hit;
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        SeleccionarPersonaje();
        SeleccionarEnemigo();
    }
    private void SeleccionarPersonaje()
    {
        //PC
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, longitudInfinita, layerMaskPersonaje))
            {
                if (hit.collider != null)
                {
                    //PersonajeSeleccionado = hit.collider.GetComponent<PersonajeInteracion>();
                    //EventoPersonajeSeleccionado?.Invoke(PersonajeSeleccionado);
                }
            }
        }
        //Android 
        if (Input.touchCount == 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, longitudInfinita, layerMaskPersonaje))
            {
                if (hit.collider != null)
                {
                    //PersonajeSeleccionado = hit.collider.GetComponent<PersonajeInteracion>();
                    //EventoPersonajeSeleccionado?.Invoke(PersonajeSeleccionado);
                }
            }
        }
    }
    private void SeleccionarEnemigo()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, longitudInfinita, layerMaskEnemigo))
            {
                if (hit.collider != null)
                {
                   // EnemigoSeleccionado = hit.collider.GetComponent<EnemigoInteraccion>();
                    //Debug.Log("Objeto inpacto: " + EnemigoSeleccionado);


                    //EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);


                }
                else
                {
                    //EventoObjetoNoSeleccionado?.Invoke();
                }
            }
        }
    }
}
