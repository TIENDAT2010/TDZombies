using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Configs")]
    [SerializeField] protected float enemyHealth = 10;
    [SerializeField] protected int moveSpeed = 10;
    [SerializeField] protected int chaseSpeed = 15;

    [Header("Enemy References")]
    [SerializeField] protected EnemyID enemyID = EnemyID.Zombie_1;
    [SerializeField] protected EnemyIdleController idleController = null;
    [SerializeField] protected EnemyWalkController walkController = null;
    [SerializeField] protected EnemyChaseController chaseController = null;
    [SerializeField] protected EnemyAttackController attackController = null;
    [SerializeField] protected EnemyDeadController deadController = null;
    [SerializeField] protected NavMeshAgent navMeshAgent = null;
    [SerializeField] protected SpriteRenderer spriteRenderer = null;
    [SerializeField] protected AudioSource audioSource = null;
    [SerializeField] protected Collider2D col2D = null;
    [SerializeField] protected Color normalColor = Color.white;
    [SerializeField] protected Color fireColor = Color.red;
    [SerializeField] protected Image healthBar = null;
    [SerializeField] protected Image health = null;

    public EnemyID EnemyID => enemyID;

    public float EnemyHealth => enemyHealth;
    public int MoveSpeed => moveSpeed;
    public int ChaseSpeed => chaseSpeed;


    public Collider2D Colider => col2D;
    public NavMeshAgent NavMeshAgent => navMeshAgent;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public PlayerController PlayerController { protected set; get; }
    public WaypointController WaypointController { protected set; get; }

    public virtual void OnInit(WaypointController waypoint) { }


    public virtual void OnNextState(EnemyState nextState) { }
    

    public virtual void OnReceiveNormalDamage(float damage, Vector2 attackDir) { }

    public virtual void OnReceiveFireDamage(float firstDamage, float damagePerSecond, float timeDamageFire) { }


    public virtual bool IsInRangeChase()
    {
        return false;
    }    

    public virtual bool IsInRangeAttack()
    {
        return false;
    }

    public virtual bool PlayerIsInView()
    {
        return false ;
    }


    public virtual void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }


    public virtual void PlaySoundEffect(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
