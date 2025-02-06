using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] protected EnemyController enemyController = null;
    protected bool IsEnterState = false;

    /// <summary>
    /// Chay state Attack cua enemy.
    /// </summary>
    public virtual void EnterEnemyAttack() { }

    /// <summary>
    /// Thoat khoi state Attack cua Enemy.
    /// </summary>
    public virtual void ExitEnemyAttack() { }


    /// <summary>
    /// Lay Range Attack
    /// </summary>
    /// <returns></returns>
    public virtual float GetRangeAttack() { return 0; }
}

