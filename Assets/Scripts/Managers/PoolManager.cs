using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private PlayerController playerPrefab = null;
    [SerializeField] private BulletController bulletPrefab = null;
    [SerializeField] private BulletEffectController bulletEffectPrefab = null;
    [SerializeField] private BossBodyController bossBodyPrefab = null;
    [SerializeField] private EnemyController[] enemyPrefabs = null;
    [SerializeField] private EnemyItemController[] enemyItemPrefabs = null;
    [SerializeField] private EffectController[] effectPrefabs = null;

    private List<BulletController> listBullet = new List<BulletController>();
    private List<EnemyController> listEnemy = new List<EnemyController>();
    private List<EffectController> listBloodEffect = new List<EffectController>();
    private List<EnemyItemController> listEnemyItem = new List<EnemyItemController>();
    private List<BossBodyController> listBossBody = new List<BossBodyController>();
    private List<BulletEffectController> listBulletEffect = new List<BulletEffectController>();


    private void Awake()
    {
        if (Instance != null)
        {
            Instance = null;
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    public PlayerController GetPlayerController()
    {
        PlayerController prefab = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        prefab.gameObject.SetActive(true);
        return prefab;
    }


    public BulletController GetBulletController()
    {
        BulletController resultBullet = listBullet.Where(a => !a.gameObject.activeSelf).FirstOrDefault();
        if (resultBullet == null)
        {
            BulletController prefab = bulletPrefab;
            resultBullet = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            listBullet.Add(resultBullet);
        }
        resultBullet.gameObject.SetActive(true);
        return resultBullet;
    }



    public BulletEffectController GetBulletEffectController()
    {
        BulletEffectController resultBulletEffect = listBulletEffect.Where(a => !a.gameObject.activeSelf).FirstOrDefault();
        if (resultBulletEffect == null)
        {
            BulletEffectController prefab = bulletEffectPrefab;
            resultBulletEffect = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            listBulletEffect.Add(resultBulletEffect);
        }
        resultBulletEffect.gameObject.SetActive(true);
        return resultBulletEffect;
    }


    public EnemyController GetEnemyController(EnemyID enemyID)
    {
        EnemyController enemy = listEnemy.Where(a => a.EnemyID.Equals(enemyID) && !a.gameObject.activeSelf).FirstOrDefault();
        if (enemy == null)
        {
            EnemyController prefab = enemyPrefabs.Where(a => a.EnemyID.Equals(enemyID)).FirstOrDefault();
            enemy = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            listEnemy.Add(enemy);
        }
        enemy.gameObject.SetActive(true);
        return enemy;
    }


    public EnemyItemController GetEnemyItemController(EnemyID enemyID)
    {
        EnemyItemController enemyItem = listEnemyItem.Where(a => a.EnemyID.Equals(enemyID) && !a.gameObject.activeSelf).FirstOrDefault();
        if (enemyItem == null)
        {
            EnemyItemController prefab = enemyItemPrefabs.Where(a => a.EnemyID.Equals(enemyID)).FirstOrDefault();
            enemyItem = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            listEnemyItem.Add(enemyItem);
        }
        enemyItem.gameObject.SetActive(true);
        return enemyItem;
    }



    public EffectController GetEffectController(EffectType effectType)
    {
        EffectController resultEffect = listBloodEffect.Where(a => a.EffectType.Equals(effectType) && !a.gameObject.activeSelf).FirstOrDefault();
        if (resultEffect == null)
        {
            EffectController prefab = effectPrefabs.Where(a => a.EffectType.Equals(effectType)).FirstOrDefault(); ;
            resultEffect = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            listBloodEffect.Add(resultEffect);
        }
        resultEffect.gameObject.SetActive(true);
        return resultEffect;
    }


    public BossBodyController GetBossBodyController()
    {
        BossBodyController bossbody = listBossBody.Where(a => !a.gameObject.activeSelf).FirstOrDefault();
        if (bossbody == null)
        {
            bossbody = Instantiate(bossBodyPrefab, Vector3.zero, Quaternion.identity);
            listBossBody.Add(bossbody);
        }
        bossbody.gameObject.SetActive(true);
        return bossbody;
    }
}
