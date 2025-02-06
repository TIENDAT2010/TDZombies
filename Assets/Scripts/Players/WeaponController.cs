using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    [SerializeField] protected LayerMask enemyLayerMask = new LayerMask();
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected AudioSource audioSource = null;
    [SerializeField] protected AudioClip weaponAudio = null;
    [SerializeField] protected Transform bulletSpawnPos = null;
    [SerializeField] protected Sprite[] animationSprite;
    [SerializeField] protected Material normalMaterial = null;
    [SerializeField] protected Material whiteMaterial = null;

    protected bool IsFinishAttack = true;

    public WeaponType WeaponType => weaponType;

    public virtual void OnEnterAttack(){}

    public virtual void OnExitAttack(){}

    public virtual void OnAttack(){}

    public virtual void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public virtual void SetMaterial()
    {
        StartCoroutine(ChangeMaterial());
    }    

    private IEnumerator ChangeMaterial()
    {
        spriteRenderer.material = whiteMaterial;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material = normalMaterial;
    }
}
