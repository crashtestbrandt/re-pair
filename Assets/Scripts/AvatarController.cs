using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarController : MonoBehaviour
{
    public float speed = 0.5f;
    public float jumpSpeed = 5f;
    public float maxThrust = 1000.0f;
    public SpriteRenderer sprite;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
    }

    public void OnMove(InputValue inputVal) {
        float x = inputVal.Get<Vector2>().x;
        if (x < 0) sprite.flipX = false;
        else if (x > 0) sprite.flipX = true;
        // else x := 0

        transform.position += new Vector3(speed * inputVal.Get<Vector2>().x, 0.0f, 0.0f);

    }

    public void OnGamepadButtonDown(InputValue inputVal) {
        var gamepad = Gamepad.current;
        if (gamepad.aButton.wasPressedThisFrame) {
            Debug.Log("You pressed A!");
            rb.velocity += new Vector2(0.0f, jumpSpeed);

        }
    }
}
