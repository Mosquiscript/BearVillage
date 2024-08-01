using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    [SerializeField] private bool invert;
    [SerializeField] private bool zeroY;

    private Transform mainCameraTransform;

    private void Awake() {
        mainCameraTransform = Camera.main.transform;
    }

    private void Update() {
        LookAt();
    }

    private void OnEnable() {
        LookAt();
    }

    private void LookAt() {
        if (invert) {
            Vector3 dir = (transform.position - mainCameraTransform.position).normalized;
            transform.LookAt(transform.position + dir);
        } else {
            transform.LookAt(mainCameraTransform.position);
        }

        if (zeroY) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
        }
    }

    public void SetInvert(bool invert) {
        this.invert = invert;
    }

    public void SetZeroY(bool zeroY) {
        this.zeroY = zeroY;
    }

}
