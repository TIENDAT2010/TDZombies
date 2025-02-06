using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfiguration/LevelConfig", order = 1)]
public class LevelConfigSO : ScriptableObject
{
    public float HealthPlayer = 0f;
    public float SpeedPlayer = 0f;
    public string MapID = "Map_1";
    public int cashRewards = 0;
    public AudioClip backGroundMusic = null;
}

