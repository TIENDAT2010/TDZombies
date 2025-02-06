using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : WeaponController
{
    [SerializeField] private float damage = 0f;
    public override void OnEnterAttack()
    {
        if(IsFinishAttack)
        {
            IsFinishAttack = false;
            StartCoroutine(CRPlayAnimation());
            StartCoroutine(BatAttack());
        }
    }

    public override void OnAttack()
    {
        if(IsFinishAttack)
        {
            IsFinishAttack = false;
            StartCoroutine(CRPlayAnimation());
            StartCoroutine(BatAttack());
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
            yield return new WaitForSeconds(0.05f);
        }
        spriteRenderer.sprite = animationSprite[0];
    }

    private IEnumerator BatAttack()
    {
        yield return new WaitForSeconds(0.3f);
        Collider2D enemyCollider2D = Physics2D.OverlapCircle(transform.position, 6f, enemyLayerMask);
        if(enemyCollider2D != null)
        {
            if(PlayerIsInView(enemyCollider2D.gameObject.transform))
            {
                EnemyController enemy = enemyCollider2D.gameObject.GetComponent<EnemyController>();
                enemy.OnReceiveNormalDamage(damage, transform.up);
            }
        }
        audioSource.PlayOneShot(weaponAudio);
        yield return new WaitForSeconds(0.7f);
        IsFinishAttack = true;
    }

    private bool PlayerIsInView(Transform enemyPos)
    {
        Vector2 fowardPlayer = (enemyPos.position - transform.position).normalized;
        float dotProduct = Vector2.Dot(transform.up, fowardPlayer);
        if (dotProduct > 0)
        {
            float angle = Vector2.Angle(transform.up, fowardPlayer);
            return (angle < 60) ? true : false;
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
