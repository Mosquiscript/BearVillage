using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;

    private GridData floorData, buildingData;

    private Renderer previewRenderer;

    private List<GameObject> placedGameObjects = new();
    private void Start()
    {
        StopPlacement();
        floorData = new GridData();
        buildingData = new GridData();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if(selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.onClicked += PlaceStructure;
        inputManager.onExit += StopPlacement;

    }

    private void PlaceStructure()
    {
        if(inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
            return;
        /*mousePosition.x = mousePosition.x - 1;
        mousePosition.z = mousePosition.z - 1;
        mouseIndicator.transform.position = mousePosition;*/
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);
        placedGameObjects.Add( newObject );
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : buildingData;  //Aqui lo dejare asi, quizas modificar DB
        selectedData.AddObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size,database.objectsData[selectedObjectIndex].ID,placedGameObjects.Count -1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : buildingData;  //Aqui lo dejare asi, quizas modificar DB
        return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.onClicked -= PlaceStructure;
        inputManager.onExit -= StopPlacement;
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition); //probablemnte cambiar el ajuste arriba de aqui
        
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewRenderer.material.color = placementValidity ? Color.white : Color.red;

        mousePosition.x = mousePosition.x - 1;
        mousePosition.z = mousePosition.z - 1;
        mouseIndicator.transform.position = mousePosition;  
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
