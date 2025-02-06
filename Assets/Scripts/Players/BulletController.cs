using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask = new LayerMask();
    [SerializeField] private LayerMask obstacleLayerMask = new LayerMask();
    private float damage = 0;
    
    public void OnInit(float dg)
    {
        damage = dg;
        StartCoroutine(MoveBullet());
    }

    private IEnumerator MoveBullet()
    {
        float speed = 60f;
        while (gameObject.activeSelf == true)
        {           
            //Di chuyen dan
            transform.position += transform.up * speed * Time.deltaTime;

            //Kiem tra va cham voi Enemy
            Collider2D enemyCollider2D = Physics2D.OverlapCircle(transform.position + transform.up * 0.25f, 0.15f, enemyLayerMask);
            if (enemyCollider2D != null)
            {
                //Enemy take damage
                EnemyController enemy = enemyCollider2D.gameObject.GetComponent<EnemyController>();
                enemy.OnReceiveNormalDamage(1f, transform.up);
                gameObject.SetActive(false);
            }

            //Kiem tra va cham voi Obstacle
            Collider2D obstacleCollider2D = Physics2D.OverlapCircle(transform.position + transform.up * 0.25f, 0.15f, obstacleLayerMask);
            if (obstacleCollider2D != null)
            {
                BulletEffectController bulletEffect = PoolManager.Instance.GetBulletEffectController();
                bulletEffect.transform.position = transform.position;
                bulletEffect.PlayBulletExplodeEffect();
                gameObject.SetActive(false);
            }


            Vector2 viewPort = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPort.y > 1f || viewPort.y <0 || viewPort.x > 1f || viewPort.x <0)
            {
                gameObject.SetActive(false);
            }

            yield return null;
        }
    }

}
