using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyItemController : MonoBehaviour
{
    [SerializeField] private EnemyID enemyID = EnemyID.Zombie_1;
    [SerializeField] private Text amountText = null;


    public EnemyID EnemyID => enemyID;

    public void SetupAmount(int killed, int total)
    {
        amountText.text = killed.ToString() + "/" + total.ToString();
    }
}
