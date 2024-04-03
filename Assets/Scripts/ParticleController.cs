using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
     [Header("Movement Particle")]
     [SerializeField] ParticleSystem movementParticle;

     [Range(0, 10)]
     [SerializeField] int occurAfterVelocity;

     [Range(0, 0.2f)]
     [SerializeField] float dustFormationPeriod;

     [SerializeField] Rigidbody2D playerRb;
          
     float counter;
     bool isOnGround;

     [Header("")]
     [SerializeField] ParticleSystem fallParticle;
     [SerializeField] ParticleSystem touchParticle;

     private void Start() {
          touchParticle.transform.parent = null;
     }

     private void Update() {
          counter += Time.deltaTime;

          if (isOnGround && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity) {
               if (counter > dustFormationPeriod) {
                    movementParticle.Play();
                    counter = 0;
               }
          }
     }

     public void OnTriggerTouch2D(Vector2 pos) {
          touchParticle.transform.position = pos;
          touchParticle.Play();
     }

     private void OnTriggerEnter2D(Collider2D collision) {
          if (collision.CompareTag("Ground")) {
               fallParticle.Play();
               isOnGround = true;
          }
     }

     private void OnTriggerExit2D(Collider2D collision) {
          if (collision.CompareTag("Ground")) {
               isOnGround = false;
          }
     }
}
