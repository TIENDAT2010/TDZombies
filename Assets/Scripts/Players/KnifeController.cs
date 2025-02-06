using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{
    [SerializeField] private float damage = 0f;
    public override void OnEnterAttack()
    {
        if(IsFinishAttack)
        {
            IsFinishAttack = false;
            StartCoroutine(CRPlayAnimation());
        }
    }

    public override void OnAttack()
    {
        if(IsFinishAttack)
        {
            IsFinishAttack = false;
            StartCoroutine(CRPlayAnimation());
        }
    }

    public override void OnExitAttack()
    {

    }


    private IEnumerator CRPlayAnimation()
    {
        for (int i = 0; i < animationSprite.Length; i++)
        {
            spriteRenderer.sprite = animationSprite[i];
            if (i > 0 && animationSprite.Length / i == 2)
            {
                StartCoroutine(KnifeAttack());
            }
            yield return new WaitForSeconds(0.05f);
        }
        spriteRenderer.sprite = animationSprite[0];
    }

    private IEnumerator KnifeAttack()
    {
        
        Collider2D enemyCollider2D = Physics2D.OverlapCircle(transform.position, 4f, enemyLayerMask);
        if (enemyCollider2D != null)
        {
            if (PlayerIsInView(enemyCollider2D.gameObject.transform))
            {
                EnemyController enemy = enemyCollider2D.gameObject.GetComponent<EnemyController>();
                enemy.OnReceiveNormalDamage(damage, transform.up);
            }
        }
        audioSource.PlayOneShot(weaponAudio);
        yield return new WaitForSeconds(0.5f);
        IsFinishAttack = true;
    }

    private bool PlayerIsInView(Transform enemyPos)
    {
        Vector2 fowardPlayer = (enemyPos.position - transform.position).normalized;
        float dotProduct = Vector2.Dot(transform.up, fowardPlayer);
        if (dotProduct > 0)
        {
            float angle = Vector2.Angle(transform.up, fowardPlayer);
            return (angle < 45) ? true : false;
        }
        else
        {
            return false;
        }
    }

    public override void SetMaterial()
    {
        base.SetMaterial();
    }
}
