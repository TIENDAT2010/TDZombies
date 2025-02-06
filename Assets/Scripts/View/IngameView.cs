using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameView : BaseView
{
    [SerializeField] Image weapon = null;
    [SerializeField] Text healthText = null;
    [SerializeField] RectTransform tutorialPanel = null;
    [SerializeField] RectTransform panel_1 = null;
    [SerializeField] RectTransform panel_2 = null;
    [SerializeField] RectTransform leftFoot = null;
    [SerializeField] RectTransform rightFoot = null;
    [SerializeField] Image mainPlayer = null;
    [SerializeField] Sprite[] attackSprites = null;
    [SerializeField] Sprite[] weaponImages = null;



    private IEnumerator MoveFoots()
    {
        float t = 0;
        float moveTime = 0.25f;
        Vector2 leftFootStartPos = new Vector2(-40f, -30f);
        Vector2 leftFootEndPos = new Vector2(-40f, -80f);
        Vector2 rightFootStartPos = new Vector2(30f, -30f);
        Vector2 rightFootEndPos = new Vector2(30f, -80f);
        while (true)
        {
            t = 0;
            while (t < moveTime)
            {
                t += Time.deltaTime;
                float factor = t / moveTime;
                leftFoot.anchoredPosition = Vector2.Lerp(leftFootStartPos, leftFootEndPos, factor);
                rightFoot.anchoredPosition = Vector2.Lerp(rightFootEndPos, rightFootStartPos, factor);
                yield return null;
            }

            yield return new WaitForSeconds(0.05f);

            t = 0;
            while (t < moveTime)
            {
                t += Time.deltaTime;
                float factor = t / moveTime;
                leftFoot.anchoredPosition = Vector2.Lerp(leftFootEndPos, leftFootStartPos, factor);
                rightFoot.anchoredPosition = Vector2.Lerp(rightFootStartPos, rightFootEndPos, factor);
                yield return null;
            }
        }
    }


    private IEnumerator PlayAttackSprites()
    {
        while (panel_2.gameObject.activeSelf)
        {
            for (int i = 0; i < attackSprites.Length; i++)
            {
                mainPlayer.sprite = attackSprites[i];
                yield return new WaitForSeconds(0.05f);
            }
        }
    }



    public override void OnShow()
    {
        for (int i = 0; i < weaponImages.Length; i++)
        {
            if(weaponImages[i].name == PlayerDataController.GetCurrentWeapon().ToString()) 
            {
                weapon.sprite = weaponImages[i];
                break;
            }
        }

        if (!PlayerDataController.IsShowTutorial())
        {
            tutorialPanel.gameObject.SetActive(true);
            panel_1.gameObject.SetActive(true);
            panel_2.gameObject.SetActive(false);
            StartCoroutine(MoveFoots());
        }
        else
        {
            tutorialPanel.gameObject.SetActive(false);
        }
    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
    }


    public void UpdateHealth(float current, float total)
    {
        healthText.text = Mathf.RoundToInt(current).ToString() + "/" + Mathf.RoundToInt(total).ToString();
    }


    public void OnClickClosePanel_1()
    {
        panel_1.gameObject.SetActive(false);
        panel_2.gameObject.SetActive(true);
        StartCoroutine(PlayAttackSprites());
    }

    public void OnClickClosePanel_2()
    {
        panel_1.gameObject.SetActive(false);
        panel_2.gameObject.SetActive(false);
        tutorialPanel.gameObject.SetActive(false);
        PlayerDataController.SetShownTutorial();
    }

    public void OnClickHomeBtn()
    {
        SceneManager.LoadScene("Home");
    }

    public void OnClickNextBtn()
    {
        int currenlevel = PlayerPrefs.GetInt(PlayerPrefsKey.LEVEL_KEY);
        if(currenlevel < 5)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.LEVEL_KEY, currenlevel + 1);
            LevelConfigSO levelConfig = PlayerDataController.GetLevelConfig();
            SceneManager.LoadScene(levelConfig.MapID);
        }
    }
}
