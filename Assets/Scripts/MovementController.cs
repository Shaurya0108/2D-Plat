using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour {
    Rigidbody2D rb;
    bool btnPressed;

    [SerializeField] int speed;
    [Range(1, 10)]
    [SerializeField] float acceleration;
    float speedMultiplier;

    // flip character
    bool isWallTouched;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;

    Vector2 relativeTransform;

    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    public ParticleController particleController;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        UpdateRelativeTransform();
    }

    private void FixedUpdate() {
        UpdateSpeedMultiplier();
        float targetSpeed = speed * speedMultiplier * relativeTransform.x;

        if (isOnPlatform) {
            rb.velocity = new Vector2(targetSpeed + platformRb.velocity.x, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }

        isWallTouched = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.06f, 0.4f), 0, wallLayer);
        if (isWallTouched) {
            Flip();
        }
    }

    public void Flip() {

        particleController.OnTriggerTouch2D(wallCheckPoint.position);

        transform.Rotate(0, 180, 0);
        UpdateRelativeTransform();
    }

    void UpdateRelativeTransform() {
        relativeTransform = transform.InverseTransformVector(Vector2.one);
    }

    public void Move(InputAction.CallbackContext value) {
        if (value.started) {
            btnPressed = true;
            speedMultiplier = 1;
        } else if (value.canceled) {
            btnPressed = false;
            speedMultiplier = 0;
        }
    }

    void UpdateSpeedMultiplier() {
        if (btnPressed && speedMultiplier < 1) {
            speedMultiplier += Time.deltaTime * acceleration;
        } else if (!btnPressed && speedMultiplier > 0) {
            speedMultiplier -= Time.deltaTime * acceleration;

            if (speedMultiplier < 0) {
                speedMultiplier = 0;
            }
        }
    }
}
