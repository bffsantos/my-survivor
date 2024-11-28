using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private float _moveSpeed = 5.0f;
    private float _damage = 5.0f;
    private float _health = 40.0f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 moveDir;

    public Transform target;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
        _moveSpeed = moveSpeed;
        _damage = damage;
        _health = health;
        _animator.runtimeAnimatorController = animController;
    }

    public void OnDamage(float damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            if(_animator != null)
                SetAnimationParam("Death", true);
            
            target = null;
                        
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Player playerScript = collision.GetComponent<Player>();

            if (playerScript != null)
            {
                damageable.OnDamage(_damage);
            }
        }
    }

    private void SetAnimationParam(string key, bool value)
    {
        if (_animator != null)
            _animator.SetBool(key, value);
    }
}
