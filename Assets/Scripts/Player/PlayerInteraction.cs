using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject selectionFx;
    public bool IsSelected { get; set; }
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
    private void PlayerSelected(GameObject player)
    {
        //Comprobamos si el objeto selecionado es un jugador, si no es que no se haga el codigo
        if(player.GetComponent<PlayerInteraction>() == null)
        {
            return;
        }
        //Si esta seleccionado el personaje y damos click a un recurso o edificio que cambie el estado 
        IsSelected = true;
        ShowPlayerSelected(true);
        UIManager.Instance.ShowPanelInfoPlayer(true);
    }
    //Metodo para deseleccionar al personaje
    private void PlayerNoSelected()
    {
        IsSelected = false;
        ShowPlayerSelected(false);
        UIManager.Instance.ShowPanelInfoPlayer(false);
    }

    private void OnEnable()
    {
        SelectionManager.EventObjectSelected += PlayerSelected;
        SelectionManager.EventObjectNoSelected += PlayerNoSelected;
    }
    private void OnDisable()
    {
        SelectionManager.EventObjectSelected -= PlayerSelected;
        SelectionManager.EventObjectNoSelected -= PlayerNoSelected;
    }
}
