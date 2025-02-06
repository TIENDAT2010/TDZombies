using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossWalkController : EnemyWalkController
{
    Vector2 targetPos = Vector2.zero;


    public override void EnterEnemyWalk()
    {
        IsEnterState = true;
        float distance = Vector2.Distance(transform.position, enemyController.WaypointController.CurrentPoint);
        targetPos = (distance < 0.5f) ? enemyController.WaypointController.GetNextPoint() : enemyController.WaypointController.CurrentPoint;
        StartCoroutine(PlayAnimationWalk());
    }

    public override void ExitEnemyWalk()
    {
        IsEnterState = false;
        StopAllCoroutines();
    }


    private void Update()
    {
        if (IsEnterState)
        {
            //kiem tra va chuyen sang trang thai chase
            if(enemyController.IsInRangeChase())
            {
                enemyController.OnNextState(EnemyState.Chase_State);
            }


            if (targetPos != Vector2.zero)
            {
                var navMeshPath = new NavMeshPath();
                enemyController.NavMeshAgent.CalculatePath(targetPos, navMeshPath);
                if (enemyController.NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    if (navMeshPath.corners.Length >= 2)
                    {
                        Vector2 targetDir = (navMeshPath.corners[1] - transform.position).normalized;
                        transform.up = Vector2.Lerp(transform.up, targetDir, 20 * Time.deltaTime);

                        Vector2 currentPos = transform.position;
                        currentPos += (Vector2)transform.up * enemyController.MoveSpeed * Time.deltaTime;
                        transform.position = currentPos;

                    }
                }

                if (Vector2.Distance(transform.position, targetPos) < 0.2f)
                {
                    enemyController.OnNextState(EnemyState.Idle_State);
                }
            }
        }
    }




    /// <summary>
    /// Chay animation walk.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayAnimationWalk()
    {
        yield return null;
        while(IsEnterState)
        {
            for (int i = 0; i < animationSprites.Length; i++)
            {
                enemyController.SpriteRenderer.sprite = animationSprites[i];
                yield return new WaitForSeconds(0.1f);

                float distance = Vector2.Distance(targetPos, transform.position);
                if(distance < 0.1f)
                {
                    enemyController.SpriteRenderer.sprite = animationSprites[0];
                    yield break;
                }
            }
        }
    }    
}
