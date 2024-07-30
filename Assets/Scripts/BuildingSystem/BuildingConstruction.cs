using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BuildingConstruction : MonoBehaviour {
    /* public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingTypeSO) {
        Transform buildingTransform = Instantiate(ChopChopAssets.Instance.pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.Setup(buildingTypeSO);

        return buildingConstruction;
    } */
 

    private ObjectsDatabaseSO buildingTypeSO;
    private World_Bar constructionBar;
    private float progress;

    private void Setup(ObjectsDatabaseSO buildingTypeSO) {
        this.buildingTypeSO = buildingTypeSO;

        constructionBar = World_Bar.Create(transform, new Vector3(0, 4, 0), new Vector3(3, .2f), Color.grey, Color.yellow, 1f, 0, new World_Bar.Outline { color = Color.black, size = .1f });
        /* LookAtCamera lookAtCamera = constructionBar.GetGameObject().AddComponent<LookAtCamera>();
        lookAtCamera.SetInvert(true); */

        constructionBar.SetSize(0f);

        /* Transform visualTransform = Instantiate(buildingTypeSO.ObjectData.visual); */
        /* visualTransform.SetParent(transform);
        visualTransform.localPosition = Vector3.zero;
        visualTransform.eulerAngles = Vector3.zero; */

        /* SetLayerRecursive(visualTransform.gameObject, 15); */
    }

    public void AddProgress(float addAmount) {
        progress += addAmount;

        /* constructionBar.SetSize(progress / buildingTypeSO.ObjectData.constructionProgressMax); */

        /* if (IsConstructed()) {
             Transform buildingTransform = Instantiate(buildingTypeSO.ObjectData.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject); 
        } */
    }

    /* public bool IsConstructed() {
        return progress >= buildingTypeSO.ObjectData.constructionProgressMax;
    }
 */
    public Vector3 GetPosition() {
        return transform.position;
    }

    /* public float GetConstructionDistanceOffset() {
        return buildingTypeSO.ObjectData.constructionDistanceOffset;
    } */

    private void SetLayerRecursive(GameObject targetGameObject, int layer) {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

}
