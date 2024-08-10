using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitSelectionManager : MonoBehaviour {

    [SerializeField] private Transform selectionAreaTransform = null;


    private Vector3 startPosition;
    private List<PlayerController> selectedUnitList;
    private Camera mainCamera;
    [SerializeField] private InputSystemManager inputSystemManager;

    private void Awake() {
        selectedUnitList = new List<PlayerController>();
        selectionAreaTransform.gameObject.SetActive(false);
        mainCamera = Camera.main;
        inputSystemManager = InputSystemManager.Instance;
    }

     private void OnEnable()
    {
        //Cuando se suscribe en el evento nos manda 2 parametros que son Vector2 y el tiempo
        /* inputSystemManager.OnStartTouch += SelectedUnid; */
    }
    private void OnDisable()
    {
        /* inputSystemManager.OnEndTouch -= SelectedUnid; */
    }

    private void Update() 
    {
      
        if (Input.GetMouseButtonDown(0))
        {
            SelectedUnid();
        }

        if (Input.touchCount == 2)
            {
                SelectedUnid();
            }
    }

    private void SelectedUnid(/* Vector2 screenPosition, float time */)
    {
        /* DeselectAllUnits(); */
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit)) 
                {   
                   
                    if (raycastHit.collider.TryGetComponent<PlayerController>(out PlayerController playerController)) 
                    {
                         Debug.Log("Click "+ raycastHit.collider + "Is enemy: "+ playerController.IsEnemy());
                        if (!playerController.IsEnemy()) {
                            playerController.SetIsSelected(true);
                            selectedUnitList.Add(playerController);
                        }
                    }
                }
    }



    private void DeselectAllUnits() {
        foreach (PlayerController playerController in selectedUnitList) {
            if (playerController.IsDead()) continue; // Dead
            playerController.SetIsSelected(false);
        }

        selectedUnitList.Clear();
    }

    public List<PlayerController> GetSelectedUnitList() {
        return selectedUnitList;
    }

}
