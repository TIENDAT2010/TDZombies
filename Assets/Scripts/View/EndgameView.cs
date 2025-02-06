
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameView : BaseView
{
    [SerializeField] private GameObject victoryText = null;
    [SerializeField] private GameObject defeatedText = null;
    [SerializeField] private RectTransform contentTrans = null;
    [SerializeField] private RectTransform nextLevelBtn = null;
    [SerializeField] private RectTransform replayBtn = null;
    [SerializeField] private GameObject cashPanel = null;
    [SerializeField] private Text cashText = null;

    public override void OnShow()
    {
        if(PlayerController.IsDefeated == false)
        {
            replayBtn.gameObject.SetActive(false);
            nextLevelBtn.gameObject.SetActive(true);
            victoryText.SetActive(true);
            defeatedText.SetActive(false);
            cashPanel.SetActive(true);
            LevelConfigSO levelConfig = PlayerDataController.GetLevelConfig();
            cashText.text = "+ " + levelConfig.cashRewards.ToString();
            PlayerDataController.UpdateCash(levelConfig.cashRewards);
        }
        else
        {
            replayBtn.gameObject.SetActive(true);
            nextLevelBtn.gameObject.SetActive(false);
            victoryText.SetActive(false);
            defeatedText.SetActive(true);
            cashPanel.SetActive(false);
        }
    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
    }



    public void UpdateKilledEnemy(Dictionary<EnemyID, int> dicInitEnemy, Dictionary<EnemyID, int> dicKilledEnemy)
    {
        foreach (KeyValuePair<EnemyID, int> keyValuePair in dicInitEnemy)
        {
            EnemyItemController enemyItem = PoolManager.Instance.GetEnemyItemController(keyValuePair.Key);
            enemyItem.transform.SetParent(contentTrans);
            enemyItem.transform.localScale = Vector3.one;
            enemyItem.SetupAmount(dicKilledEnemy.ContainsKey(keyValuePair.Key) ? dicKilledEnemy[keyValuePair.Key] : 0, keyValuePair.Value);
        }
    }



    public void OnClickHomeButton()
    {
        SceneManager.LoadScene("Home");
    }

    public void OnClickWeaponButton()
    {
        ViewManager.Instance.SetActiveView(ViewType.WeaponsView);
    }

    public void OnClickNextLevel()
    {
        LevelConfigSO levelConfig = PlayerDataController.GetLevelConfig();
        SceneManager.LoadScene(levelConfig.MapID);
    }
}
