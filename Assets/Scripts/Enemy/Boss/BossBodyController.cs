using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBodyController : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask = new LayerMask();
    [SerializeField] private LayerMask obstacleLayerMask = new LayerMask();
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private BossBodyConfig[] bossBodyConfigs = null;

    private BossBodyConfig selectedConfigs = null;
    private float damage = 0;

    public void OnInit(float dg, int id)
    {
        damage = dg;
        foreach (BossBodyConfig config in bossBodyConfigs)
        {
            if(config.ID == id)
            {
                selectedConfigs = config;
                break;
            }
                
        }
        spriteRenderer.sprite = selectedConfigs.BodySprite;
        StartCoroutine(MoveClaw());
    }

    private IEnumerator MoveClaw()
    {
        float speed = 25f;
        while (gameObject.activeSelf == true)
        {           
            //Di chuyen dan
            transform.position += transform.up * speed * Time.deltaTime;

            foreach (Transform check in selectedConfigs.colPos)
            {
                //Kiem tra va cham voi Player
                Collider2D enemyCollider2D = Physics2D.OverlapCircle(check.position + transform.up * 0.25f, 0.15f, playerLayerMask);
                if (enemyCollider2D != null)
                {
                    enemyCollider2D.GetComponent<PlayerController>().OnReceiveNormalAttack(damage, transform.up);
                    gameObject.SetActive(false);
                }

                //Kiem tra va cham voi Obstacle
                Collider2D obstacleCollider2D = Physics2D.OverlapCircle(check.position + transform.up * 0.25f, 0.15f, obstacleLayerMask);
                if (obstacleCollider2D != null)
                {

                    gameObject.SetActive(false);
                }
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
