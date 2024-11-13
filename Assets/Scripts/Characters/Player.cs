using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float health = 100.0f;

    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private PlayerEventsScriptableObject _playerEvent;
    
    private Rigidbody2D _rigidbody;
    
    private Animator _animator;
    
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 moveDir;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.y = Input.GetAxis("Vertical");

        PlayerAnimation();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        Movement();
    }

    private void Movement()
    {
        _rigidbody.MovePosition(_rigidbody.position + moveDir * _moveSpeed * Time.fixedDeltaTime);

        if (transform.position.x <= -14.5f)
        {
            transform.position = new Vector2(-14.5f, transform.position.y);
        }
        else if (transform.position.x >= 14.5f)
        {
            transform.position = new Vector2(14.5f, transform.position.y);
        }
        else if (transform.position.y <= -14.5f)
        {
            transform.position = new Vector2(transform.position.x, -14.5f);
        }
        else if (transform.position.y >= 14.5f)
        {
            transform.position = new Vector2(transform.position.x, 14.5f);
        }
    }

    private void Shoot()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        Vector3 inputPosition = new Vector3(mouseX, mouseY, 10.0f);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(inputPosition);
        Vector3 distanceVector = mousePosition - transform.position;
        Vector3 projectileDir = distanceVector.normalized;

        GameObject projectileGameObject = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.moveDir = projectileDir;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        projectileGameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void PlayerAnimation()
    {
        if (moveDir != Vector2.zero)
        {
            _animator.SetBool("Move", true);

            if (moveDir.x > 0) _spriteRenderer.flipX = true;
            else if (moveDir.x < 0) _spriteRenderer.flipX = false;

        }
        else
        {
            _animator.SetBool("Move", false);
        }
    }

    private void OnDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            gameObject.SetActive(false);

            GameState gameSate = FindObjectOfType<GameState>();

            gameSate.GameOver();

            return;
        }

        _playerEvent.HealthChanged(health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            OnDamage(enemy.damage);
        }
    }
}
