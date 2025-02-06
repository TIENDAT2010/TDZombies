using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField] private EnemyID enemyID = EnemyID.Zombie_1;
    [SerializeField] Transform[] waypoints = null;

    public EnemyID EnemyID => enemyID;

    private bool isForward = true;
    private int waypointIndex = 0;

    public Vector2 CurrentPoint => waypoints[waypointIndex].position;

    public Vector2 GetNextPoint()
    {
        if (isForward)
        {
            if (waypointIndex < waypoints.Length - 1)
            {
                waypointIndex++;
            }
            else if (waypointIndex == waypoints.Length - 1)
            {
                waypointIndex--;
                isForward = false;
            }
        }
        else
        {
            if (waypointIndex > 0)
            {
                waypointIndex--;
            }
            else
            {
                waypointIndex++;
                isForward = true;
            }
        }

        return waypoints[waypointIndex].position;
    }

    public void CreateEnemy()
    {
        EnemyController enemy = PoolManager.Instance.GetEnemyController(enemyID);
        enemy.transform.position = waypoints[0].position;
        enemy.OnInit(this);    
    }
}
