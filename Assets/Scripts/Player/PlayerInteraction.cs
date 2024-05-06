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

    private void PlayerSelected(PlayerInteraction playerInteraction)
    {
        IsSelected = true;
        ShowPlayerSelected(true);
        UIManager.Instance.ShowPanelInfoPlayer(true);
    }

    private void PlayerNoSelected()
    {
        IsSelected = false;
        ShowPlayerSelected(false);
        UIManager.Instance.ShowPanelInfoPlayer(false);
    }

    private void OnEnable()
    {
        SelectionManager.EventPlayerSelected += PlayerSelected;
        SelectionManager.EventPlayerObjectNoSelected += PlayerNoSelected;
    }
    private void OnDisable()
    {
        SelectionManager.EventPlayerSelected -= PlayerSelected;
        SelectionManager.EventPlayerObjectNoSelected -= PlayerNoSelected;
    }
}
