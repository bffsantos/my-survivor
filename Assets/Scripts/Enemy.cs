using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float damage = 5.0f;
    public float health = 40.0f;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public Transform target;

    private Vector2 moveDir;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            moveDir = (target.position - transform.position).normalized;

            if (moveDir.x < 0) spriteRenderer.flipX = true;
            else if (moveDir.x > 0) spriteRenderer.flipX = false;
        }
        else
        {
            moveDir = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
