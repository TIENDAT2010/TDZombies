using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        ViewManager.Instance.SetActiveView(ViewType.HomeView);
        PlayerDataController.InitPlayerData();
    }
}
