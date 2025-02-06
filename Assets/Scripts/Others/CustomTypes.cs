using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    GameInit = 0,
    GameStart = 1,
    GamePause = 2,
    LevelFailed = 3,
    LevelCompleted = 4,
}


public enum PlayerState
{
    PlayerStart = 0,
    PlayerVictory = 1,
    PlayerDefeated = 2,
}


public enum ViewType
{
    HomeView = 0,
    IngameView = 1,
    EndgameView = 2,
    WeaponsView = 3,
}


public enum WeaponType
{
    Bat = 0,
    Knife = 1,
    HandGun = 2,
    Riffle = 3,
    FlameThrower = 4,
}

public enum EnemyState
{
    Idle_State = 0,
    Walk_State = 1,
    Chase_State = 2,
    Attack_State = 3,
    Dead_State = 4,
}

public enum EnemyID
{
    Zombie_1 = 0,
    Zombie_2 = 1,
    Zombie_3 = 2,
    Zombie_4 = 3,
    Zombie_5 = 4,
    Zombie_6 = 5,
    Zombie_7 = 6,
    Zombie_8 = 7,
    Boss_1 = 8,
    Boss_2 = 9,
    Boss_3 = 10,
}

public enum AttackType
{
    Normal_Attack = 0,
    Fire_Attack = 1,
    Poison_Attack = 2,
    Claw_Attack = 3,
    Hand_Attack = 4,
    Body_Attack = 5,
}

public enum EffectType
{
    Zombie_Blood_Effect = 0,
    Boss_Blood_Effect = 1,
    Enemy_Burn_Effect = 2,
    Player_Blood_Effect = 3,
    Player_Burn_Effect = 4,
    Player_Poison_Effect = 5,
}

[System.Serializable]
public class WeaponTypeConfig
{
    public WeaponType WeaponType = WeaponType.Knife;
    public Sprite[] AnimationSprites = null;
}

[System.Serializable]
public class AttackConfig
{
    public AttackType attackType = AttackType.Normal_Attack;
    public float rangeAttack = 3f;
    public float firstDamage = 1f;
    public float damagePerSecond = 0.5f;
    public float damageAffectTime = 5f;
    public SpriteRenderer WarningSprite = null;
    public Transform attackPos = null;
    public AudioClip audioClip = null;
    public Sprite[] animSprites = null;
}

[System.Serializable]
public class BossBodyConfig
{
    public int ID = 0;
    public Sprite BodySprite = null;
    public Transform[] colPos = null;
}