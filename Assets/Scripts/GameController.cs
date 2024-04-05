using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    Vector2 startPos;
    SpriteRenderer spriteRenderer;
    
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
       startPos = transform.position; 
    }

    private void OnTriggerenter2D(Collider2D collision) {
        if (collision.CompareTag("Obstacle")) {
            Die();
        }
    }

    void Die() {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration) {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        spriteRenderer.enabled = true;
    }
}
