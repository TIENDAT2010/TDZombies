using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeadController : EnemyDeadController
{
    public override void EnterEnemyDead()
    {
        IsEnterState = true;
        Destroy(enemyController.NavMeshAgent);
        Destroy(enemyController.Colider);
        StartCoroutine(PlayAnimationDead());
    }

    public override void ExitEnemyDead()
    {
        IsEnterState = false;
        StopAllCoroutines();
    }

    private IEnumerator PlayAnimationDead()
    {
        yield return null;
        for (int i = 0; i < animationSprites.Length; i++)
        {
            enemyController.SpriteRenderer.sprite = animationSprites[i];
            yield return new WaitForSeconds(0.1f);
        }

        enemyController.PlayerController.UpdateDeadEnemy(enemyController.EnemyID);
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
