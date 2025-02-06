using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkController : MonoBehaviour
{
    [SerializeField] protected EnemyController enemyController = null;
    [SerializeField] protected Sprite[] animationSprites = null;

    protected bool IsEnterState = false;

    
    /// <summary>
    /// Chay state Walk cua enemy.
    /// </summary>
    public virtual void EnterEnemyWalk() { }

    /// <summary>
    /// Thoat state Walk cua enemy.
    /// </summary>
    public virtual void ExitEnemyWalk() { }
}

