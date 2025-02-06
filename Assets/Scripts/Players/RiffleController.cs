using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiffleController : WeaponController
{
    [SerializeField] private float damageBullet = 5f;

    public override void OnEnterAttack()
    {
        if (IsFinishAttack)
        {
            IsFinishAttack = false;
            StartCoroutine(CRPlayAnimation());
            StartCoroutine(RiffleAttack());
        }
    }

    public override void OnAttack()
    {
        if (IsFinishAttack)
        {
            IsFinishAttack = false;
            StartCoroutine(CRPlayAnimation());
            StartCoroutine(RiffleAttack());
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

    private IEnumerator RiffleAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            BulletController bulletspawn = PoolManager.Instance.GetBulletController();
            bulletspawn.transform.position = bulletSpawnPos.transform.position;
            bulletspawn.transform.up = transform.up;
            bulletspawn.OnInit(damageBullet);
            yield return new WaitForSeconds(0.15f);
            audioSource.PlayOneShot(weaponAudio);
        }
        IsFinishAttack = true;
    }

    public override void SetMaterial()
    {
        base.SetMaterial();
    }
}
