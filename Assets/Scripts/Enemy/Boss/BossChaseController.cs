using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossChaseController : EnemyChaseController
{

    public override void EnterEnemyChase()
    {
        IsEnterState = true;
        StartCoroutine(PlayAnimation());
    }

    public override void ExitEnemyChase()
    {
        IsEnterState = false;
        StopAllCoroutines();
    }

    private IEnumerator PlayAnimation()
    {
        yield return null;
        int animIndex = 0;
        while (IsEnterState)
        {
            enemyController.SpriteRenderer.sprite = animationSprites[animIndex];
            yield return new WaitForSeconds(0.15f);
            animIndex = (animIndex == animationSprites.Length - 1) ? 0 : animIndex + 1;
        }
    }


    private void Update()
    {
        if(IsEnterState)
        {
            var navMeshPath = new NavMeshPath();
            enemyController.NavMeshAgent.CalculatePath(enemyController.PlayerController.transform.position, navMeshPath);
            if(enemyController.NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                if (navMeshPath.corners.Length >= 2)
                {
                    Vector2 targetDir = (navMeshPath.corners[1] - transform.position).normalized;
                    transform.up = Vector2.Lerp(transform.up, targetDir, 20 * Time.deltaTime);

                    Vector2 currentPos = transform.position;
                    currentPos += (Vector2)transform.up * enemyController.ChaseSpeed * Time.deltaTime;
                    transform.position = currentPos;

                }
            }

            if(enemyController.IsInRangeChase() == false)
            {
                enemyController.SpriteRenderer.sprite = animationSprites[0];
                enemyController.OnNextState(EnemyState.Walk_State);
            }
            if(enemyController.IsInRangeAttack())
            {
                enemyController.SpriteRenderer.sprite = animationSprites[0];
                enemyController.OnNextState(EnemyState.Attack_State);
            }
        }   
    }
}
