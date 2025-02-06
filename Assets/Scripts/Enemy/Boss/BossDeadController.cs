using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadController : EnemyDeadController
{
    [SerializeField] private AudioClip deadAudio = null;

    public override void EnterEnemyDead()
    {
        IsEnterState = true;
        enemyController.PlaySoundEffect(deadAudio);
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
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
