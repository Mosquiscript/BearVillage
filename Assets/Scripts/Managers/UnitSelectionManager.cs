using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitSelectionManager : MonoBehaviour {

    [SerializeField] private Transform selectionAreaTransform = null;


    private Vector3 startPosition;
    private List<PlayerController> selectedUnitList;

    private void Awake() {
        selectedUnitList = new List<PlayerController>();
        selectionAreaTransform.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) /* && !UtilsClass.IsPointerOverUI() */) {
            selectionAreaTransform.gameObject.SetActive(true);
            startPosition = Mouse3D.GetMouseWorldPosition();

            DeselectAllUnits();
        }

        if (Input.GetMouseButton(0)) {
            // Left Mouse Button Held Down
            CalculateSelectionLowerLeftUpperRight(out Vector3 lowerLeft, out Vector3 upperRight);
            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;
        }

        if (Input.GetMouseButtonUp(0)) {
            // Hide visual even if over the UI
            selectionAreaTransform.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0) /* && !UtilsClass.IsPointerOverUI() */) {
            CalculateSelectionLowerLeftUpperRight(out Vector3 lowerLeft, out Vector3 upperRight);

            // Calculate Center and Extents
            Vector3 selectionCenterPosition = new Vector3(
                lowerLeft.x + ((upperRight.x - lowerLeft.x) / 2f),
                0,
                lowerLeft.z + ((upperRight.z - lowerLeft.z) / 2f)
            );

            Vector3 halfExtents = new Vector3(
                (upperRight.x - lowerLeft.x) * .5f,
                1,
                (upperRight.z - lowerLeft.z) * .5f
            );

            // Set min size
            float minSelectionSize = .5f;
            if (halfExtents.x < minSelectionSize) halfExtents.x = minSelectionSize;
            if (halfExtents.z < minSelectionSize) halfExtents.z = minSelectionSize;

            // Find Objects within Selection Area
            Collider[] colliderArray = Physics.OverlapBox(selectionCenterPosition, halfExtents);
            foreach (Collider collider in colliderArray) {

                if (collider.TryGetComponent<PlayerController>(out PlayerController playerController)) {
                    if (!playerController.IsEnemy()) {
                        playerController.SetIsSelected(true);
                        selectedUnitList.Add(playerController);
                    }
                }
            }
        }
    }

    private void CalculateSelectionLowerLeftUpperRight(out Vector3 lowerLeft, out Vector3 upperRight) {
        Vector3 currentMousePosition = Mouse3D.GetMouseWorldPosition();
        lowerLeft = new Vector3(
            Mathf.Min(startPosition.x, currentMousePosition.x),
            0,
            Mathf.Min(startPosition.z, currentMousePosition.z)
        );
        upperRight = new Vector3(
            Mathf.Max(startPosition.x, currentMousePosition.x),
            0,
            Mathf.Max(startPosition.z, currentMousePosition.z)
        );
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
