using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyController
{
    private bool isDead = false;
    private bool isFire = false;
    private float firstHealth = 0;
    private float currentHideTimer = 0f;

    private void Update()
    {
        if (enemyHealth <= 0 && isDead == false)
        {
            isDead = true;
            OnNextState(EnemyState.Dead_State);
        }
        healthBar.fillAmount = enemyHealth / firstHealth;


        currentHideTimer -= Time.deltaTime;
        if(currentHideTimer > 0)
        {
            health.gameObject.SetActive(true);
        }
        else
        {
            health.gameObject.SetActive(false);
        }       
    }


    public override void OnInit(WaypointController waypointController)
    {
        PlayerController = FindObjectOfType<PlayerController>();
        healthBar.fillAmount = 1f;
        health.gameObject.SetActive(false);
        firstHealth = enemyHealth;
        WaypointController = waypointController;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.Warp(WaypointController.CurrentPoint);
        OnNextState(EnemyState.Idle_State);
    }


    public override void OnReceiveNormalDamage(float damage, Vector2 attackDir)
    {
        //Tao blood effect
        EffectController zombieBloodEffect = PoolManager.Instance.GetEffectController(EffectType.Zombie_Blood_Effect);
        zombieBloodEffect.transform.position = transform.position;
        zombieBloodEffect.OnInit(attackDir, true);

        ///Xu ly hp
        enemyHealth = enemyHealth - damage;

        ///Xu ly time healthbar
        currentHideTimer = 2f;
    }


    private IEnumerator HandleFireEffect(float damagePerSecond, EffectController fireEffect, float timeDamageFire)
    {
        while (timeDamageFire > 0 && enemyHealth > 0) 
        {
            yield return new WaitForSeconds(1f);
            enemyHealth -= damagePerSecond;
            timeDamageFire = timeDamageFire - 1f;

            ///Xu ly time healthbar
            currentHideTimer = 2f;
        }
        isFire = false;
        fireEffect.transform.SetParent(null);
        fireEffect.gameObject.SetActive(false);
        SetColor(normalColor);
    }


    public override void OnReceiveFireDamage(float firstDamage, float damagePerSecond, float timeDamageFire)
    {
        enemyHealth -= firstDamage;
        if (isFire == false)
        {
            SetColor(fireColor);
            isFire = true;
            EffectController fireEffect = PoolManager.Instance.GetEffectController(EffectType.Enemy_Burn_Effect);
            fireEffect.transform.SetParent(transform);
            fireEffect.transform.localPosition = Vector2.zero;
            fireEffect.OnInit(transform.up, false);
            StartCoroutine(HandleFireEffect(damagePerSecond, fireEffect, timeDamageFire));
        }

        ///Xu ly time healthbar
        currentHideTimer = 2f;
    }


    public override bool IsInRangeChase()
    {
        float distance = Vector2.Distance(PlayerController.transform.position, transform.position);
        if (distance < 20f)
        {
            return true;
        }
        return false;
    }



    public override bool IsInRangeAttack()
    {
        float distance = Vector2.Distance(PlayerController.transform.position, transform.position);
        if (distance < attackController.GetRangeAttack())
        {
            return true;
        }
        return false;
    }



    public override bool PlayerIsInView()
    {
        Vector2 fowardPlayer = (PlayerController.transform.position - transform.position).normalized;
        float dotProduct = Vector2.Dot(transform.up, fowardPlayer);
        if (dotProduct > 0)
        {
            float angle = Vector2.Angle(transform.up, fowardPlayer);
            return (angle < 60) ? true : false;
        }
        else
        {
            return false;
        }
    }



    public override void OnNextState(EnemyState nextState)
    {
        if(nextState == EnemyState.Idle_State)
        {
            idleController.EnterEnemyIdle();
            deadController.ExitEnemyDead();
            walkController.ExitEnemyWalk();
            attackController.ExitEnemyAttack();
            chaseController.ExitEnemyChase();
        }
        else if(nextState == EnemyState.Walk_State)
        {
            idleController.ExitEnemyIdle();
            deadController.ExitEnemyDead();
            walkController.EnterEnemyWalk();
            attackController.ExitEnemyAttack();
            chaseController.ExitEnemyChase();
        }
        else if (nextState == EnemyState.Dead_State)
        {
            idleController.ExitEnemyIdle();
            deadController.EnterEnemyDead();
            walkController.ExitEnemyWalk();
            attackController.ExitEnemyAttack();
            chaseController.ExitEnemyChase();
        }
        else if (nextState == EnemyState.Attack_State)
        {
            idleController.ExitEnemyIdle();
            deadController.ExitEnemyDead();
            walkController.ExitEnemyWalk();
            attackController.EnterEnemyAttack();
            chaseController.ExitEnemyChase();
        }
        else if( nextState == EnemyState.Chase_State)
        {
            idleController.ExitEnemyIdle();
            deadController.ExitEnemyDead();
            walkController.ExitEnemyWalk();
            attackController.ExitEnemyAttack();
            chaseController.EnterEnemyChase();
        }
    }
}
