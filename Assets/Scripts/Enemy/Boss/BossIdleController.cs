using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleController : EnemyIdleController
{

    public override void EnterEnemyIdle()
    {
        IsEnterState = true;
        StartCoroutine(PlayAnimation());
        StartCoroutine(CheckPlayerInRange());
    }

    public override void ExitEnemyIdle()
    {
        IsEnterState = false;
        StopAllCoroutines();
    }


    private IEnumerator PlayAnimation()
    {
        yield return null;
        for (int i = 0; i < animationSprites.Length; i++)
        {
            enemyController.SpriteRenderer.sprite = animationSprites[i];
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        enemyController.OnNextState(EnemyState.Walk_State);
    }

    private IEnumerator CheckPlayerInRange()
    {
        while(IsEnterState)
        {          
            if(enemyController.IsInRangeChase())
            {
                enemyController.OnNextState(EnemyState.Chase_State);
                yield break;
            }
            yield return null;
        }
    }    
}
