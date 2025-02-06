using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] WaypointController[] waypointControllers = null;
    [SerializeField] Transform playerPos = null;
    [SerializeField] CinemachineVirtualCamera virtualCamera = null;
    [SerializeField] AudioSource audioSource = null;
    private void Start()
    {
        LevelConfigSO levelConfig = PlayerDataController.GetLevelConfig();
        audioSource.clip = levelConfig.backGroundMusic;
        audioSource.Play();
        PlayerController player = PoolManager.Instance.GetPlayerController();
        player.transform.position = playerPos.position;
        player.OnInit();
        virtualCamera.Follow = player.transform;

        for (int i = 0; i < waypointControllers.Length; i++)
        {
            player.AddInitEnemy(waypointControllers[i].EnemyID);
            waypointControllers[i].CreateEnemy();
        }
    }
}
