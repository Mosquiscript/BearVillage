using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public EnemyInteraction EnemyObjective { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnemyRangeSelected(GameObject enemySelected)
    {
        if (EnemyObjective == enemySelected.GetComponent<EnemyInteraction>())
        {
            return;
        }
        EnemyObjective = enemySelected.GetComponent<EnemyInteraction>();
        EnemyObjective.ShowEnemySelected(true);
    }
    private void EnemyNoSelected()
    {
        if (EnemyObjective == null)
        {
            return;
        }
        EnemyObjective.ShowEnemySelected(false);
        EnemyObjective = null;
        
    }
    private void OnEnable()
    {
        SelectionManager.EventObjectSelected += EnemyRangeSelected;
        SelectionManager.EventObjectNoSelected += EnemyNoSelected;
    }
    private void OnDisable()
    {
        SelectionManager.EventObjectSelected -= EnemyRangeSelected;
        SelectionManager.EventObjectNoSelected -= EnemyNoSelected;
    }

   
}
