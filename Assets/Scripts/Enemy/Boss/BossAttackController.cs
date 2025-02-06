using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackController : EnemyAttackController
{
    [SerializeField] private LayerMask playerLayer = new LayerMask();
    [SerializeField] private AttackConfig[] attackConfigs = null;


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
        if (selectedAttack == null)
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

        if (selectedAttack != null && selectedAttack.attackType == AttackType.Normal_Attack)
        {
            StartCoroutine(PlayNormalAttack());
        }
        else if (selectedAttack != null && selectedAttack.attackType == AttackType.Fire_Attack)
        {
            StartCoroutine(ShowWarning(selectedAttack.WarningSprite, AttackType.Fire_Attack));
        }
        else if(selectedAttack != null && selectedAttack.attackType == AttackType.Claw_Attack)
        {
            StartCoroutine(ShowWarning(selectedAttack.WarningSprite, AttackType.Claw_Attack));
        }    
        else if(selectedAttack != null && selectedAttack.attackType == AttackType.Hand_Attack)
        {
            StartCoroutine(ShowWarning(selectedAttack.WarningSprite, AttackType.Hand_Attack));
        }
        else if (selectedAttack != null && selectedAttack.attackType == AttackType.Body_Attack)
        {
            StartCoroutine(ShowWarning(selectedAttack.WarningSprite, AttackType.Body_Attack));
        }
    }



    /// <summary>
    /// Hien canh bao va xu ly Attack
    /// </summary>
    /// <param name="warning"></param>
    /// <param name="attackType"></param>
    /// <returns></returns>
    private IEnumerator ShowWarning(SpriteRenderer warning, AttackType attackType)
    {
        float t = 0;
        float moveTime = 0.15f;
        Color startColor = new Color(warning.color.r, warning.color.g, warning.color.b, 0f);
        Color endColor = new Color(warning.color.r, warning.color.g, warning.color.b, 0.5f);
        for(int i = 0; i < 4; i++)
        {
            if(i == 0 || i == 2)
            {
                while (t < moveTime)
                {
                    t += Time.deltaTime;
                    float factor = t / moveTime;
                    Color newColor = Color.Lerp(startColor, endColor, factor);
                    warning.color = newColor;
                    yield return null;
                }
                t = 0;
            }   
            else
            {
                while (t < moveTime)
                {
                    t += Time.deltaTime;
                    float factor = t / moveTime;
                    Color newColor = Color.Lerp(endColor, startColor, factor);
                    warning.color = newColor;
                    yield return null;
                }
                t = 0;
            }    
        }

        if(attackType == AttackType.Fire_Attack)
        {
            StartCoroutine(PlayFireAttack());
        }
        else if(attackType == AttackType.Claw_Attack)
        {
            StartCoroutine(PlayClawAttack());
        }
        else if(attackType == AttackType.Hand_Attack)
        {
            StartCoroutine(PlayHandAttack());
        }
        else if(attackType == AttackType.Body_Attack)
        {
            StartCoroutine(PlayBodyAttack());
        }
    }

   

    /// <summary>
    /// Xu ly Normal Attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayNormalAttack()
    {
        yield return null;
        if (selectedAttack != null && selectedAttack.attackType == AttackType.Normal_Attack)
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
                    else if(i == 3) { enemyController.PlaySoundEffect(selectedAttack.audioClip); }
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
    /// Xu ly Fire Attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayFireAttack()
    {
        yield return null;
        if (selectedAttack != null && selectedAttack.attackType == AttackType.Fire_Attack)
        {
            enemyController.PlaySoundEffect(selectedAttack.audioClip);
            for (int i = 0; i < selectedAttack.animSprites.Length; i++)
            {
                enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[i];

                if (i >= 5 && i <= 8)
                {
                    RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, 0.7f, transform.up, selectedAttack.rangeAttack, playerLayer);
                    if (raycastHit2D.collider != null)
                    {
                        enemyController.PlayerController.OnReceiveFireAttack(selectedAttack.firstDamage, selectedAttack.damageAffectTime, selectedAttack.damageAffectTime);
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



    private IEnumerator PlayClawAttack()
    {
        yield return null;
        if (selectedAttack != null && selectedAttack.attackType == AttackType.Claw_Attack)
        {
            for (int i = 0; i < selectedAttack.animSprites.Length; i++)
            {
                enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[i];
                if (i == 6)
                {
                    BossBodyController claw = PoolManager.Instance.GetBossBodyController();
                    claw.transform.position = selectedAttack.attackPos.position;
                    claw.transform.up = transform.up;
                    claw.OnInit(selectedAttack.firstDamage, 1);
                }
                else if (i == 3) { enemyController.PlaySoundEffect(selectedAttack.audioClip); }
                yield return new WaitForSeconds(0.05f);
            }
            enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[0];
            yield return new WaitForSeconds(0.5f);
            selectedAttack = null;
            enemyController.OnNextState(EnemyState.Walk_State);
        }
    }



    private IEnumerator PlayHandAttack()
    {
        yield return null;
        if (selectedAttack != null && selectedAttack.attackType == AttackType.Hand_Attack)
        {
            for (int i = 0; i < selectedAttack.animSprites.Length; i++)
            {
                enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[i];

                if (i == 3)
                {
                    BossBodyController hand = PoolManager.Instance.GetBossBodyController();
                    hand.transform.position = selectedAttack.attackPos.position;
                    hand.transform.up = transform.up;
                    hand.OnInit(selectedAttack.firstDamage, 2);
                }
                else if (i == 2) { enemyController.PlaySoundEffect(selectedAttack.audioClip); }
                yield return new WaitForSeconds(0.05f);
            }
            enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[0];
            yield return new WaitForSeconds(0.5f);
            selectedAttack = null;
            enemyController.OnNextState(EnemyState.Walk_State);
        }
    }

    private IEnumerator PlayBodyAttack()
    {
        yield return null;
        if (selectedAttack != null && selectedAttack.attackType == AttackType.Body_Attack)
        {
            for (int i = 0; i < selectedAttack.animSprites.Length; i++)
            {
                enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[i];

                if (i == 8)
                {
                    if(enemyController.EnemyID == EnemyID.Boss_1)
                    {
                        BossBodyController body = PoolManager.Instance.GetBossBodyController();
                        body.transform.position = selectedAttack.attackPos.position;
                        body.transform.up = transform.up;
                        body.OnInit(selectedAttack.firstDamage, 3);
                    }
                    else if(enemyController.EnemyID == EnemyID.Boss_2)
                    {
                        BossBodyController body = PoolManager.Instance.GetBossBodyController();
                        body.transform.position = selectedAttack.attackPos.position;
                        body.transform.up = transform.up;
                        body.OnInit(selectedAttack.firstDamage, 4);
                    }
                    
                }
                else if (i == 2) { enemyController.PlaySoundEffect(selectedAttack.audioClip); }
                yield return new WaitForSeconds(0.05f);
            }
            enemyController.SpriteRenderer.sprite = selectedAttack.animSprites[0];
            yield return new WaitForSeconds(0.5f);
            selectedAttack = null;
            enemyController.OnNextState(EnemyState.Walk_State);
        }
    }
}
