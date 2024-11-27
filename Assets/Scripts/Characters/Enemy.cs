using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _moveSpeed = 5.0f;
    private float _damage = 5.0f;
    private float _health = 40.0f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 moveDir;

    public Transform target;

    // Update is called once per frame
    private void Update()
    {
        if (target)
        {
            moveDir = (target.position - transform.position).normalized;

            if (moveDir.x < 0) _spriteRenderer.flipX = true;
            else if (moveDir.x > 0) _spriteRenderer.flipX = false;
        }
        else
        {
            moveDir = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + moveDir * _moveSpeed * Time.fixedDeltaTime);
    }

    public void InitializeData(float moveSpeed, float damage, float health, AnimatorController animController, Sprite sprite)
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _moveSpeed = moveSpeed;
        _damage = damage;
        _health = health;
        _animator.runtimeAnimatorController = animController;
        _spriteRenderer.sprite = sprite;
    }

    public void OnDamage(float damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            _animator.SetBool("Death", true);
            
            target = null;
                        
            Destroy(gameObject, 1f);
        }
    }
}
