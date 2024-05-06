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
    private void EnemyRangeSelected(EnemyInteraction enemySelected)
    {
        if (EnemyObjective == enemySelected)
        {
            return;
        }
        EnemyObjective = enemySelected;
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
        SelectionManager.EventEnemySelected += EnemyRangeSelected;
        SelectionManager.EventEnemyObjectNoSelected += EnemyNoSelected;
    }
    private void OnDisable()
    {
        SelectionManager.EventEnemySelected -= EnemyRangeSelected;
        SelectionManager.EventEnemyObjectNoSelected -= EnemyNoSelected;
    }

   
}
