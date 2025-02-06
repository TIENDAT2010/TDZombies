using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFadeController : MonoBehaviour
{
    private PlayerController playerController = null;
    private SpriteRenderer spriteRenderer = null;
    private Color originalColor = Color.white;
    private float distanceToFade = 0f;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalColor = spriteRenderer.color;
        }
        distanceToFade = spriteRenderer.bounds.size.y;
    }

    private void Update()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
        else
        {
            float distance = Vector2.Distance(playerController.transform.position, transform.position);
            float fadeAlpha = Mathf.Clamp(distance / distanceToFade, 0.3f, 1f);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, fadeAlpha);
        }
    }
}
