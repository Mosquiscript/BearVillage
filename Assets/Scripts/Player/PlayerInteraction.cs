using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject selectionFx;
    public bool IsSelected { get; set; }
    
    private void Awake() {
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void ShowPlayerSelected(bool status)
    {
        selectionFx.SetActive(status);
    }
    //Metodo para seleccionar al personaje
    private void PlayerSelected(GameObject objactSelected)
    {
        //Comprobamos si el objeto selecionado es un jugador
        if(objactSelected.GetComponent<PlayerInteraction>() != null)
        {
            //Si esta seleccionado el personaje y damos click a un recurso o edificio que cambie el estado 
         /*    IsSelected = true;
            ShowPlayerSelected(true);
            UIManager.Instance.ShowPanelInfoPlayer(true); */
        }
        if (objactSelected.CompareTag("Resource"))
        {
            Debug.Log("Es un recurso");
           
        }


        
    }
    //Metodo para deseleccionar al personaje

}
