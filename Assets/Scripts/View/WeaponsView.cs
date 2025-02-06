using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponsView : BaseView
{
    [SerializeField] Button leftButton = null;
    [SerializeField] Button rightButton = null;
    [SerializeField] Button unlockButton = null;
    [SerializeField] Button equipButton = null;
    [SerializeField] Text cashText = null;
    [SerializeField] WeaponItemController[] itemController;

    [SerializeField] Text priceUnlock = null;
    [SerializeField] GameObject pricePanel = null;

    private int weaponIndex = 0;
    public override void OnShow()
    {
        cashText.text = PlayerDataController.GetCurrentCash().ToString();
        unlockButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        pricePanel.SetActive(false);

        WeaponType currentWeapon = PlayerDataController.GetCurrentWeapon();
        for(int i = 0; i < itemController.Length; i++)
        {
            if(itemController[i].WeaponType == currentWeapon)
            {
                weaponIndex = i;
                itemController[i].gameObject.SetActive(true);
                itemController[i].OnInit();
            }
            else
            {
                itemController[i].gameObject.SetActive(false);
            }         
        }
        leftButton.gameObject.SetActive(weaponIndex > 0);
        rightButton.gameObject.SetActive(weaponIndex < itemController.Length - 1);
    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
    }


    public void OnClickHomeButton()
    {
        ViewManager.Instance.SetActiveView(ViewType.HomeView);
    }

    public void OnClickUnlockButton()
    {
        for(int i = 0; i < itemController.Length; i++)
        {
            if(i == weaponIndex)
            {
                if (int.Parse(cashText.text) >= itemController[weaponIndex].PriceUnlock)
                {
                    cashText.text = (int.Parse(cashText.text) - itemController[weaponIndex].PriceUnlock).ToString();
                    PlayerDataController.UpdateUnlockWeapon(itemController[weaponIndex].WeaponType);
                    PlayerDataController.UpdateCash(-itemController[weaponIndex].PriceUnlock);
                    unlockButton.gameObject.SetActive(false);
                    equipButton.gameObject.SetActive(true);
                    pricePanel.SetActive(false);
                }
                break;
            }           
        }
    }

    public void OnClickEquipButton()
    {
        for (int i = 0; i < itemController.Length; i++)
        {
            PlayerDataController.UpdateCurrentWeapon(itemController[weaponIndex].WeaponType);
        }
        unlockButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
    }


    public void OnClickLeftButton()
    {
        for(int i = 0; i < itemController.Length; i++)
        {
            itemController[weaponIndex].gameObject.SetActive(false);
        }
        weaponIndex--;
        for(int i = 0; i < itemController.Length; i++)
        {
            if(i == weaponIndex)
            {
                itemController[weaponIndex].gameObject.SetActive(true);
                itemController[weaponIndex].OnInit();
                if (itemController[weaponIndex].WeaponType == PlayerDataController.GetCurrentWeapon())
                {
                    unlockButton.gameObject.SetActive(false);
                    equipButton.gameObject.SetActive(false);
                    pricePanel.SetActive(false);
                }
                else
                {
                    bool isUnlock = PlayerDataController.IsUnlockWeapon(itemController[weaponIndex].WeaponType);
                    if (isUnlock)
                    {
                        unlockButton.gameObject.SetActive(false);
                        equipButton.gameObject.SetActive(true);
                        pricePanel.SetActive(false);
                    }
                    else
                    {
                        unlockButton.gameObject.SetActive(true);
                        equipButton.gameObject.SetActive(false);
                        priceUnlock.text = "$ " + itemController[weaponIndex].PriceUnlock.ToString();
                        pricePanel.SetActive(true);
                    }
                }
                break;
            }    
        }
        if(weaponIndex == 0)
        {
            leftButton.gameObject.SetActive(false);
        }
        rightButton.gameObject.SetActive(true);
    }


    public void OnClickRightButton()
    {
        for (int i = 0; i < itemController.Length; i++)
        {
            itemController[weaponIndex].gameObject.SetActive(false);
        }
        weaponIndex++;
        for (int i = 0; i < itemController.Length; i++)
        {
            if(i == weaponIndex)
            {
                itemController[weaponIndex].gameObject.SetActive(true);
                itemController[weaponIndex].OnInit();
                if (itemController[weaponIndex].WeaponType == PlayerDataController.GetCurrentWeapon())
                {
                    unlockButton.gameObject.SetActive(false);
                    equipButton.gameObject.SetActive(false);
                    pricePanel.SetActive(false);
                }
                else
                {
                    bool isUnlock = PlayerDataController.IsUnlockWeapon(itemController[weaponIndex].WeaponType);
                    if (isUnlock)
                    {
                        unlockButton.gameObject.SetActive(false);
                        equipButton.gameObject.SetActive(true);
                        pricePanel.SetActive(false);
                    }
                    else
                    {
                        unlockButton.gameObject.SetActive(true);
                        equipButton.gameObject.SetActive(false);
                        priceUnlock.text = "$ " + itemController[weaponIndex].PriceUnlock.ToString();
                        pricePanel.SetActive(true);
                    }
                }
                break;
            }
            
        }
        if(weaponIndex == itemController.Length-1)
        {
            rightButton.gameObject.SetActive(false);
        }
        leftButton.gameObject.SetActive(true);
    }

}
