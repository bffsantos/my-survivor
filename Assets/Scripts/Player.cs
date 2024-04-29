using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5.0f;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector2 movementDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementDir.x = Input.GetAxis("Horizontal");
        movementDir.y = Input.GetAxis("Vertical");

        if(movementDir != Vector2.zero)
        {
            if(movementDir.x > 0) spriteRenderer.flipX = true;
            else if(movementDir.x < 0) spriteRenderer.flipX = false;

            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDir * moveSpeed * Time.fixedDeltaTime);
    }
}
