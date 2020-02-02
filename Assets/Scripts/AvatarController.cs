using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarController : MonoBehaviour
{
    public int MAX_JUMPS = 2;
    public float AIR_CONTROL_FACTOR = 0.5f;
    public float BASE_MOVEMENT_SPEED = 10f;
    public float BASE_JUMP_SPEED = 20f;

    public SpriteRenderer sprite;
    private Rigidbody2D rb;
    private int numJumps = 0;
    public bool buddyJumpEnabled = false;
    public bool grounded = false;

    private float xInput = 0.0f;
    private Vector2 jumpStartVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;
        rb.velocity = new Vector2(
            BASE_MOVEMENT_SPEED * xInput,
            rb.velocity.y
        );

        if (!grounded) {
            //jumpStartVelocity = rb.velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Player upward velocity: " + rb.velocity.y.ToString());
        
        if (other.gameObject.CompareTag("Floor")) {
            if (!grounded) {
                grounded = true;
                jumpStartVelocity = Vector2.zero;
                RestoreJumps();
            }
        }
    }

    public void OnMove(InputValue inputVal) {
        xInput = grounded? inputVal.Get<Vector2>().x : AIR_CONTROL_FACTOR * inputVal.Get<Vector2>().x;
        if (xInput < 0) sprite.flipX = false;
        else if (xInput > 0) sprite.flipX = true;
        // else x := 0
    }

    public void OnGamepadButtonDown(InputValue inputVal) {
        var gamepad = Gamepad.current;
        if (gamepad.aButton.wasPressedThisFrame) {
            if (numJumps > 0) {
                if (buddyJumpEnabled) buddyJumpEnabled = false;
                else numJumps--;

                rb.velocity = new Vector2(rb.velocity.x, BASE_JUMP_SPEED);
            }

            if (grounded) {
                grounded = false;
                jumpStartVelocity = rb.velocity;
            }


        }
    }

    public void RestoreJumps() {
        numJumps = MAX_JUMPS;
    }
}
