using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadController : MonoBehaviour
{
    [SerializeField] protected EnemyController enemyController = null;
    [SerializeField] protected Sprite[] animationSprites = null;

    protected bool IsEnterState = false;


    /// <summary>
    /// Chay state Dead cua enemy.
    /// </summary>
    public virtual void EnterEnemyDead() 
    {

    }


    /// <summary>
    /// Thoat state Dead cua enemy.
    /// </summary>
    public virtual void ExitEnemyDead() { }
}

