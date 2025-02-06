using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseController : MonoBehaviour
{
    [SerializeField] protected EnemyController enemyController = null;
    [SerializeField] protected Sprite[] animationSprites = null;

    protected bool IsEnterState = false;

    /// <summary>
    /// Chay state Chase cua enemy.
    /// </summary>
    public virtual void EnterEnemyChase() { }

    /// <summary>
    /// Thoat state Chase cua enemy.
    /// </summary>
    public virtual void ExitEnemyChase() { }
}

