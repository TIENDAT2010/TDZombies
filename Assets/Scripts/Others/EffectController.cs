using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] private EffectType effectType = EffectType.Zombie_Blood_Effect;
    [SerializeField] private ParticleSystem particleEffect = null;


    public EffectType EffectType => effectType;


    /// <summary>
    /// Init effect.
    /// </summary>
    /// <param name="upDir"></param>
    /// <param name="autoDissable"></param>
    public void OnInit(Vector2 upDir, bool autoDissable)
    {
        transform.up = upDir;
        particleEffect.Play();
        if(autoDissable)
        {
            StartCoroutine(WaitDisable());
        }
    }


    /// <summary>
    /// Cho va disable effect.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitDisable()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
