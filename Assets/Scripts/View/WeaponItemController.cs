using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemController : MonoBehaviour
{
    [SerializeField] WeaponType weaponType = WeaponType.Bat;
    [SerializeField] Image image = null;
    [SerializeField] Sprite[] sprites = null;
    [SerializeField] int priceUnlock = 0; 

    public WeaponType WeaponType => weaponType;

    public int PriceUnlock => priceUnlock;

    public void OnInit()
    {
        gameObject.SetActive(true);
        StartCoroutine(PlayAnimation());
    }
    public IEnumerator PlayAnimation()
    {
        while (gameObject.activeSelf)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                image.sprite = sprites[i];
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
