using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerController : WeaponController
{
    [SerializeField] private float firstDamage = 5f;
    [SerializeField] private float damagePerSecond = 1f;
    [SerializeField] private int timeDamageFire = 5;

    [SerializeField] private Sprite[] firstSprites = null;
    [SerializeField] private Sprite[] midleSprites = null;
    [SerializeField] private Sprite[] endSprites = null;

    private bool isKeepFiring = false;
    public override void OnEnterAttack()
    {
        if(IsFinishAttack)
        {
            IsFinishAttack = false;
            audioSource.clip = weaponAudio;
            audioSource.Play();
            StartCoroutine(OnEnterAttackAnimation());
        }
    }


    public override void OnAttack()
    {
        if(!isKeepFiring)
        {
            isKeepFiring = true;
            StartCoroutine(OnAttackAnimation());
        }
    }


    public override void OnExitAttack()
    {
        StopAllCoroutines();
        StartCoroutine(OnExitAttackAnimation());
    }


    private IEnumerator OnEnterAttackAnimation()
    {
        for (int i = 0; i < firstSprites.Length; i++)
        {
            spriteRenderer.sprite = firstSprites[i];
            yield return new WaitForSeconds(0.1f);
        }

        //Check hit enemy
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(bulletSpawnPos.transform.position, 1f, transform.up, 5f, enemyLayerMask);
        if (raycastHit2D.collider != null)
        {
            EnemyController enemy = raycastHit2D.collider.gameObject.GetComponent<EnemyController>();
            enemy.OnReceiveFireDamage(firstDamage, damagePerSecond, timeDamageFire);
        }
    }

    private IEnumerator OnAttackAnimation()
    {
        while (true)
        {
            for (int i = 0; i < midleSprites.Length; i++)
            {
                spriteRenderer.sprite = midleSprites[i];
                yield return new WaitForSeconds(0.05f);
            }

            //Check hit enemy
            RaycastHit2D raycastHit2D = Physics2D.CircleCast(bulletSpawnPos.transform.position, 1f, transform.up, 5f, enemyLayerMask);
            if (raycastHit2D.collider != null)
            {
                EnemyController enemy = raycastHit2D.collider.gameObject.GetComponent<EnemyController>();
                enemy.OnReceiveFireDamage(firstDamage, damagePerSecond, timeDamageFire);
            }
        }
    }

    private IEnumerator OnExitAttackAnimation()
    {
        for (int i = 0; i < endSprites.Length; i++)
        {
            spriteRenderer.sprite = endSprites[i];
            yield return new WaitForSeconds(0.1f);
        }
        audioSource.Stop();
        isKeepFiring = false;
        IsFinishAttack = true;
    }

    public override void SetMaterial()
    {
        base.SetMaterial();
    }
}
