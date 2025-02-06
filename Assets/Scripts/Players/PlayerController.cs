using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Configs")]
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color poisonColor = Color.green;
    [SerializeField] Color fireColor = Color.red;

    [Header("Player References")]
    [SerializeField] WeaponController[] weapons = null;
    [SerializeField] WeaponController weaponControllers = null;
    [SerializeField] FootController footControllers = null;
    [SerializeField] private LayerMask ostacleLayer = new LayerMask();
    [SerializeField] private LayerMask enemyLayer = new LayerMask();
    
    public PlayerState PlayerState { get; private set; }
    public static bool IsDefeated {  get; private set; }

    private Dictionary<EnemyID, int> dictionaryKilledEnemy = new Dictionary<EnemyID, int>();
    private Dictionary<EnemyID, int> dictionaryInitEnemy = new Dictionary<EnemyID, int>();
    private Vector2 vector3MoveInput;
    private Vector2 mousePosition;
    private float currentHealth = 0;
    private float moveSpeed = 0;
    private float totalPlayerHealth = 0f;
    private bool isPoison = false;
    private bool isFire = false;
    private bool isDebuff = false;

    public void OnInit()
    {
        PlayerState = PlayerState.PlayerStart;
        ViewManager.Instance.SetActiveView(ViewType.IngameView);

        IsDefeated = false;

        LevelConfigSO levelConfig = PlayerDataController.GetLevelConfig();
        currentHealth = levelConfig.HealthPlayer;
        totalPlayerHealth = levelConfig.HealthPlayer;
        moveSpeed = levelConfig.SpeedPlayer;
        ViewManager.Instance.IngameView.UpdateHealth(currentHealth, totalPlayerHealth);
        footControllers.SetIdle();
        for(int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i].WeaponType == PlayerDataController.GetCurrentWeapon())
            {
                weaponControllers = weapons[i];
                weaponControllers.gameObject.SetActive(true);
                break;
            }
        }
    }


    public void AddInitEnemy(EnemyID enemyID)
    {
        if (dictionaryInitEnemy.ContainsKey(enemyID)) { dictionaryInitEnemy[enemyID] += 1; }
        else { dictionaryInitEnemy.Add(enemyID, 1); }
    }


    private void Update()
    {
        if(PlayerState == PlayerState.PlayerStart && PlayerDataController.IsShowTutorial())
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            vector3MoveInput = new Vector2(horizontal, vertical);

            RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, 1f, vector3MoveInput.normalized, 1.5f, (ostacleLayer | enemyLayer));
            if (raycastHit2D.collider == null)
            {
                Vector2 playerPos = transform.position;
                playerPos += vector3MoveInput * moveSpeed * Time.deltaTime;
                transform.position = playerPos;

                if (horizontal + vertical != 0 && footControllers.IsIdle == true)
                {
                    footControllers.SetMoving();
                }
                else if (vertical + horizontal == 0 && footControllers.IsIdle == false)
                {
                    footControllers.SetIdle();
                }
            }


            if (Input.GetMouseButtonDown(0))
            {
                weaponControllers.OnEnterAttack();
            }
            if (Input.GetMouseButton(0))
            {
                weaponControllers.OnAttack();
            }
            if (Input.GetMouseButtonUp(0))
            {
                weaponControllers.OnExitAttack();
            }


            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.up = (mousePosition - (Vector2)transform.position).normalized;

            if (currentHealth <= 0)
            {
                IsDefeated = true;
                PlayerState = PlayerState.PlayerDefeated;
                ViewManager.Instance.SetActiveView(ViewType.EndgameView);
                ViewManager.Instance.EndgameView.UpdateKilledEnemy(dictionaryInitEnemy, dictionaryKilledEnemy);
            }
        }
    }



    private IEnumerator HandlePoisonEffect(float damagePerSecond, EffectController poisonEffect, float timeDamagePoison)
    {
        while(timeDamagePoison > 0)
        {
            if(!isDebuff)
            {
                moveSpeed = moveSpeed / 2;
                isDebuff = true;
            }
            yield return new WaitForSeconds(1f);
            currentHealth -= damagePerSecond;
            timeDamagePoison = timeDamagePoison - 1f;
            ViewManager.Instance.IngameView.UpdateHealth(currentHealth, totalPlayerHealth);
        }
        moveSpeed = moveSpeed * 2;
        isDebuff = false;
        poisonEffect.transform.SetParent(null);
        poisonEffect.gameObject.SetActive(false);
        isPoison = false;
        weaponControllers.SetColor(normalColor);
    }



    private IEnumerator HandleFireEffect(float damagePerSecond, EffectController fireEffect, float timeDamageFire)
    {
        while(timeDamageFire > 0)
        {
            yield return new WaitForSeconds(1f);
            currentHealth -= damagePerSecond;
            timeDamageFire = timeDamageFire - 1f;
            ViewManager.Instance.IngameView.UpdateHealth(currentHealth, totalPlayerHealth);
        }
        fireEffect.transform.SetParent(null);
        fireEffect.gameObject.SetActive(false);
        isFire = false;
        weaponControllers.SetColor(normalColor);
    }



    public void OnReceiveNormalAttack(float damage, Vector2 attackDir) 
    {
        weaponControllers.SetMaterial();
        currentHealth = currentHealth - damage;
        ViewManager.Instance.IngameView.UpdateHealth(currentHealth, totalPlayerHealth);
        EffectController redEffect = PoolManager.Instance.GetEffectController(EffectType.Player_Blood_Effect);
        redEffect.transform.position = transform.position;
        redEffect.OnInit(attackDir, true);
    }



    public void OnReceiveFireAttack(float fisrtDamage, float damagePerSecond, float timeDamageFire) 
    {
        currentHealth -= fisrtDamage;
        ViewManager.Instance.IngameView.UpdateHealth(currentHealth, totalPlayerHealth);
        if (isFire == false)
        {
            weaponControllers.SetColor(fireColor);
            isFire = true;
            EffectController fireEffect = PoolManager.Instance.GetEffectController(EffectType.Player_Burn_Effect);
            fireEffect.transform.SetParent(transform);
            fireEffect.transform.localPosition = new Vector2(-0.35f, -0.45f);
            fireEffect.OnInit(transform.up, false);
            StartCoroutine(HandleFireEffect(damagePerSecond, fireEffect, timeDamageFire));
        }
    }


    public void OnReceivePoisonAttack(float fisrtDamage, float damagePerSecond, float timeDamage) 
    {
        currentHealth -= fisrtDamage;
        ViewManager.Instance.IngameView.UpdateHealth(currentHealth, totalPlayerHealth);
        if (isPoison == false)
        {
            weaponControllers.SetColor(poisonColor);
            isPoison = true;
            EffectController poisonEffect = PoolManager.Instance.GetEffectController(EffectType.Player_Poison_Effect);
            poisonEffect.transform.SetParent(transform);
            poisonEffect.transform.localPosition = new Vector2(-0.35f, -0.45f);
            poisonEffect.OnInit(transform.up, false);
            StartCoroutine(HandlePoisonEffect(damagePerSecond, poisonEffect, timeDamage));
        }
    }


    public void UpdateDeadEnemy(EnemyID enemyID)
    {
        if (dictionaryKilledEnemy.ContainsKey(enemyID)) { dictionaryKilledEnemy[enemyID] += 1; }
        else { dictionaryKilledEnemy.Add(enemyID, 1); }

        int totalInit = 0;
        int totalDeath = 0;
        foreach (KeyValuePair<EnemyID,int> keyValuePairs in dictionaryInitEnemy)
        {
            totalInit += keyValuePairs.Value;
        }
        foreach (KeyValuePair<EnemyID, int> keyValuePairs in dictionaryKilledEnemy)
        {
            totalDeath += keyValuePairs.Value;
        }


        if (totalDeath >= totalInit && PlayerState != PlayerState.PlayerDefeated)
        {
            IsDefeated = false;
            PlayerState = PlayerState.PlayerVictory;
            ViewManager.Instance.SetActiveView(ViewType.EndgameView);
            ViewManager.Instance.EndgameView.UpdateKilledEnemy(dictionaryInitEnemy, dictionaryKilledEnemy);
            int currenlevel = PlayerPrefs.GetInt(PlayerPrefsKey.LEVEL_KEY);
            PlayerPrefs.SetInt(PlayerPrefsKey.LEVEL_KEY, currenlevel + 1);
        }
    }
}
