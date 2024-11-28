using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    public float damage = 25.0f;

    public Rigidbody2D rb;

    public Vector2 moveDir;

    public GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 7.0f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Enemy enemyScript = collision.GetComponent<Enemy>();

            if (enemyScript != null) 
            {
                damageable.OnDamage(damage);

                Destroy(gameObject);
            }
        }
    }
}
