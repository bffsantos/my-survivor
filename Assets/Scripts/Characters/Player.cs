using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float health = 100.0f;

    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private FloatGameEvent _healthEvent;
    
    private PlayerInput _playerInput;
    private InputAction _inputAction;

    private Rigidbody2D _rigidbody;
    
    private Animator _animator;
    
    private SpriteRenderer _spriteRenderer;

    private Vector2 _moveDir;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerInput = GetComponent<PlayerInput>();
        _inputAction = _playerInput.actions["Move"];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = _inputAction.ReadValue<Vector2>();
        float vertical = moveInput.y;
        float horizontal = moveInput.x;

        _moveDir = new Vector2(horizontal, vertical);
        Debug.Log(_moveDir);
        PlayerAnimation();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }

        Movement();

        PlayerAnimation();
    }

    private void Movement()
    {
        _rigidbody.MovePosition(_rigidbody.position + _moveDir * _moveSpeed * Time.fixedDeltaTime);

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
        float mouseX = Mouse.current.position.value.x;
        float mouseY = Mouse.current.position.value.y;

        Vector3 inputPosition = new Vector3(mouseX, mouseY, 10.0f);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(inputPosition);
        Vector3 distanceVector = mousePosition - transform.position;
        Vector3 projectileDir = distanceVector.normalized;

        GameObject projectileGameObject = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.moveDir = projectileDir;
        projectile.owner = gameObject;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        projectileGameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void PlayerAnimation()
    {
        if (_moveDir != Vector2.zero)
        {
            SetAnimationParam("Move", true);

            if (_moveDir.x > 0) _spriteRenderer.flipX = true;
            else if (_moveDir.x < 0) _spriteRenderer.flipX = false;

        }
        else
        {
            SetAnimationParam("Move", false);
        }
    }

    public void OnDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            gameObject.SetActive(false);

            GameState gameSate = FindAnyObjectByType<GameState>();

            gameSate.GameOver();

            return;
        }

        _healthEvent.Broadcast(health);
    }

    private void SetAnimationParam(string key, bool value)
    {
        if(_animator != null)
            _animator.SetBool(key, value);
    }
}
