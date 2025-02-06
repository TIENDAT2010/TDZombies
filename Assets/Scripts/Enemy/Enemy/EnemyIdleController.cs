using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleController : MonoBehaviour
{
    [SerializeField] protected EnemyController enemyController = null;
    [SerializeField] protected Sprite[] animationSprites = null;

    protected bool IsEnterState = false;


    /// <summary>
    /// Chay state Idle cua enemy.
    /// </summary>
    public virtual void EnterEnemyIdle() { }


    /// <summary>
    /// Thoat khoi state Idle cua Enemy.
    /// </summary>
    public virtual void ExitEnemyIdle() { }



}

