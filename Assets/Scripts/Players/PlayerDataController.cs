using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataController
{
    private const string PLAYER_DATA = "playerdata";


    public static void InitPlayerData()
    {
        if(!PlayerPrefs.HasKey(PLAYER_DATA))
        {
            PlayerData playerData = new PlayerData();
            playerData.CurrentWeapon = WeaponType.Bat;
            playerData.CurrentCash = 5000;
            playerData.IsShowTutorial = 0;
            playerData.listUnlockWeapon = new List<WeaponType> { WeaponType.Bat };
            string dataplayer = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString(PLAYER_DATA, dataplayer);
            PlayerPrefs.SetInt(PlayerPrefsKey.LEVEL_KEY, 1);
        }
    }


    public static LevelConfigSO GetLevelConfig()
    {
        int CurrentLevel = PlayerPrefs.GetInt(PlayerPrefsKey.LEVEL_KEY, 1);
        LevelConfigSO levelConfig = Resources.Load("Levels/" + CurrentLevel.ToString(), typeof(LevelConfigSO)) as LevelConfigSO;
        return levelConfig;
    }


    public static bool IsShowTutorial()
    {
        string data = PlayerPrefs.GetString(PLAYER_DATA);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);
        return playerData.IsShowTutorial == 1;
    }

    public static void SetShownTutorial()
    {
        string data = PlayerPrefs.GetString(PLAYER_DATA);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);
        playerData.IsShowTutorial = 1;
        string newdata = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PLAYER_DATA, newdata);
    }


    public static int GetCurrentCash()
    {
        string data = PlayerPrefs.GetString(PLAYER_DATA);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);
        return playerData.CurrentCash;
    }

    public static void UpdateCash(int cash)
    {
        string data = PlayerPrefs.GetString(PLAYER_DATA);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);
        playerData.CurrentCash += cash;
        string newdata = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PLAYER_DATA, newdata);
    }


    public static void UpdateCurrentWeapon(WeaponType weaponType)
    {
        string data = PlayerPrefs.GetString(PLAYER_DATA);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);
        playerData.CurrentWeapon = weaponType;
        string newdata = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PLAYER_DATA, newdata);
    }


    public static void UpdateUnlockWeapon(WeaponType weaponType)
    {
        string data = PlayerPrefs.GetString(PLAYER_DATA);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);

        if(!playerData.listUnlockWeapon.Contains(weaponType))
        {
            playerData.listUnlockWeapon.Add(weaponType);
            string newdata = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString(PLAYER_DATA, newdata);
        }     
    }


    public static bool IsUnlockWeapon(WeaponType weaponType)
    {
        string data = PlayerPrefs.GetString(PLAYER_DATA);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);

        if (playerData.listUnlockWeapon.Contains(weaponType))
        {
            return true;
        }
        return false;
    }


    public static WeaponType GetCurrentWeapon()
    {
        string data = PlayerPrefs.GetString(PLAYER_DATA);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(data);
        return playerData.CurrentWeapon;
    }
}
