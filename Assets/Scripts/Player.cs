using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float health = 100.0f;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject projectilePrefab;

    public CanvasManager canvasManager;

    private Vector2 moveDir;

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
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);

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
        Vector3 projectileDir = (mousePosition - transform.position).normalized;

        GameObject projectileGameObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.moveDir = projectileDir;
    }

    private void PlayerAnimation()
    {
        if (moveDir != Vector2.zero)
        {
            animator.SetBool("Move", true);

            if (moveDir.x > 0) spriteRenderer.flipX = true;
            else if (moveDir.x < 0) spriteRenderer.flipX = false;

        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    private void OnDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            gameObject.SetActive(false);

            GameManager gameManager = FindObjectOfType<GameManager>();
            
            gameManager.GameOver();

            return;
        }

        canvasManager.UpdateHealth(health);
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
