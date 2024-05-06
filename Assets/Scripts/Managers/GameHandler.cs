using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;
    [SerializeField] private Transform goldNodeTransform;
    [SerializeField] private Transform storageTransform;

    private void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Transform GetTransformMineNode(){
        return goldNodeTransform;
    }

    public Transform GetTransformStorage(){
        return storageTransform;
    }
}
