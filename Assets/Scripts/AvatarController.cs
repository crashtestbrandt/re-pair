using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarController : MonoBehaviour
{
    public int MAX_JUMPS = 2;
    public float AIR_CONTROL_FACTOR = 0.5f;
    public float BASE_MOVEMENT_SPEED = 3f;
    public float BASE_JUMP_SPEED = 4f;

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
        rb.velocity = new Vector2(
            BASE_MOVEMENT_SPEED * xInput,
            rb.velocity.y
        );
    }

    private void OnCollisionEnter2D(Collision2D other) {        
        if (other.gameObject.CompareTag("Floor")) {
            if (!grounded) {
                grounded = true;
                rb.velocity = Vector2.zero;
                RestoreJumps();
            }
        }
    }

    public void OnMove(InputValue inputVal) {
        xInput = grounded? inputVal.Get<Vector2>().x : AIR_CONTROL_FACTOR * inputVal.Get<Vector2>().x;
        if (xInput < 0) sprite.flipX = true;
        else if (xInput > 0) sprite.flipX = false;
        // else x := 0
    }

    public void OnJump() {
        if (numJumps > 0) {
            if (buddyJumpEnabled) buddyJumpEnabled = false;
            else numJumps--;

            rb.velocity = new Vector2(rb.velocity.x, BASE_JUMP_SPEED);
        }

        if (grounded) {
            grounded = false;
        }
    }

    public void RestoreJumps() {
        numJumps = MAX_JUMPS;
    }
}
