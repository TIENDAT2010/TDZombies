using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttackController : EnemyAttackController
{
    [SerializeField] protected LayerMask playerLayer = new LayerMask();
    [SerializeField] protected AttackConfig[] attackConfigs = null;

    private AttackConfig selectedAttack = null;

    public override void EnterEnemyAttack()
    {
        IsEnterState = true;
        StartCoroutine(RotateToPlayer());
        
    }

    public override void ExitEnemyAttack()
    {
        IsEnterState = false;
        StopAllCoroutines();
    }


    public override float GetRangeAttack()
    {
        if(selectedAttack == null)
        {
            selectedAttack = attackConfigs[Random.Range(0, attackConfigs.Length)];
        }
        return selectedAttack.rangeAttack;
    }



    /// <summary>
    /// Xoay huong up toi Player va goi ham Attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateToPlayer()
    {
        float t = 0;
        float moveTime = 0.2f;
        Vector2 startVector3 = transform.up;
        Vector2 endVector3 = (enemyController.PlayerController.transform.position - transform.position).normalized;
        while (t < moveTime)
        {
            t += Time.deltaTime;
            float factor = t / moveTime;
            Vector3 newPos = Vector3.Lerp(startVector3, endVector3, factor);
            transform.up = newPos;
            yield return null;
        }

        if(selectedAttack != null && selectedAttack.attackType == AttackType.Normal_Attack)
        {
            StartCoroutine(PlayNormalAttack());
        }
        else if(selectedAttack != null && selectedAttack.attackType == AttackType.Poison_Attack)
        {
            StartCoroutine(PlayPoisonAttack());
        }
    }


    /// <summary>
    /// Xu ly Normal Attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayNormalAttack()
    {
        yield return null;
        if(selectedAttack != null && selectedAttack.attackType == AttackType.Normal_Attack)
        {
            for (int i = 0; i < selectedAttack.animSprites.Length; i++)
            {
                enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[i];

                if (i > 0)
                {
                    if (selectedAttack.animSprites.Length / i == 2 && enemyController.IsInRangeAttack() && enemyController.PlayerIsInView())
                    {
                        enemyController.PlayerController.OnReceiveNormalAttack(selectedAttack.firstDamage, transform.up);
                    }
                    if(i == 2)
                    {
                        enemyController.PlaySoundEffect(selectedAttack.audioClip);
                    }
                }
                yield return new WaitForSeconds(0.05f);
            }
            enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[0];
            yield return new WaitForSeconds(0.5f);
            selectedAttack = null;
            enemyController.OnNextState(EnemyState.Walk_State);
        }
    }


    /// <summary>
    /// Xu ly Poison Attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayPoisonAttack()
    {
        yield return null;
        if(selectedAttack != null && selectedAttack.attackType == AttackType.Poison_Attack)
        {
            for (int i = 0; i < selectedAttack.animSprites.Length; i++)
            {
                enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[i];

                if (i > 0)
                {
                    if (selectedAttack.animSprites.Length / i == 2)
                    {
                        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, 0.7f, transform.up, selectedAttack.rangeAttack, playerLayer);
                        if(raycastHit2D.collider != null)
                        {
                            enemyController.PlayerController.OnReceivePoisonAttack(selectedAttack.firstDamage, selectedAttack.damagePerSecond, selectedAttack.damageAffectTime);
                        }
                    }
                }
                else if (i == 0) { enemyController.PlaySoundEffect(selectedAttack.audioClip); }
                yield return new WaitForSeconds(0.05f);
            }
            enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[0];
            yield return new WaitForSeconds(0.5f);
            selectedAttack = null;
            enemyController.OnNextState(EnemyState.Walk_State);
        }      
    }
}
