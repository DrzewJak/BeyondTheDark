using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    
    [Header("Movement Variables")]
    public float moveSpeed = 5f;
    public float jumpHeight = 15f;
    private float direction;
    
    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        isGrounded = CheckGround();

        Jump();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocityY);
    }

    private bool CheckGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, groundCheckDistance, groundLayer);
        
        return raycastHit.collider != null;
    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpHeight);
        }
        
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocityY > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.5f);
        }
    }

}