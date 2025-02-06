using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public WeaponType CurrentWeapon = WeaponType.Knife;
    public int CurrentCash = 0;
    public int IsShowTutorial = 0;
    public List<WeaponType> listUnlockWeapon = new List<WeaponType>();
}
