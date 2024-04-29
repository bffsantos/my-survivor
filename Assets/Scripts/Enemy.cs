using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public Transform target;

    private Vector2 movementDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementDir = (target.position - transform.position).normalized;

        if (movementDir.x < 0) spriteRenderer.flipX = true;
        else if (movementDir.x > 0) spriteRenderer.flipX = false;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDir * moveSpeed * Time.fixedDeltaTime);
    }
}
