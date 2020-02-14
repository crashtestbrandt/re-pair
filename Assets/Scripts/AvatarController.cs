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

    private int jumpTicks = 0;
    public int MAX_JUMP_TICKS = 10;
    private ContactPoint2D[] contacts;

    void Start()
    {
        contacts = new ContactPoint2D[16];

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(
            BASE_MOVEMENT_SPEED * xInput,
            rb.velocity.y
        );

        if (jumpTicks > 0) {
            rb.velocity = new Vector2(rb.velocity.x, BASE_JUMP_SPEED);
            jumpTicks--;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) { 
        if (other.gameObject.CompareTag("Floor")) {
            int count = other.GetContacts(contacts);
            foreach (var contact in contacts) {
                if (Vector2.Dot(contact.normal, Vector2.up) > 0) {

                    Debug.Log("You collided with a jumpable surface!");
                    // TODO: Trigger GetJumps() event; pass transform.position and normal
                }
            }
            if (!grounded) {
                grounded = true;
                rb.velocity = Vector2.zero;
                RestoreJumps();
            }
        }
    }

    public void Move(InputAction.CallbackContext context) {
        MoveHorizontally(context.ReadValue<Vector2>().x);
    }

    private void MoveHorizontally(float c) {
        xInput = (grounded? 1.0f : AIR_CONTROL_FACTOR) * c;
        if (xInput < 0) sprite.flipX = true;
        else if (xInput > 0) sprite.flipX = false;
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.started) JumpStart();
        if (context.canceled) JumpCancel();
    }

    public void JumpStart() {
        if (numJumps > 0) {
            if (buddyJumpEnabled) buddyJumpEnabled = false;
            else numJumps--;
            jumpTicks = MAX_JUMP_TICKS;
        }

        if (grounded) {
            grounded = false;
        }
    }

    public void JumpCancel() {
        jumpTicks = 0;
    }

    public void RestoreJumps() {
        numJumps = MAX_JUMPS;
    }
}
