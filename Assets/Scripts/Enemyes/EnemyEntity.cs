using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyAI))]

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;

    public event EventHandler OnDeath;
    public event EventHandler OnTakeHit;

    private int currentHealth;

    private BoxCollider2D boxCollider2D;
    private PolygonCollider2D polygonCollider2D;
    private EnemyAI enemyAI;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemyAI = GetComponent<EnemyAI>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        currentHealth = enemySO.enemyHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }


    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            boxCollider2D.enabled = false;

            if (polygonCollider2D != null)
            {
                polygonCollider2D.enabled = false;
            }

            enemyAI.SetDeathState();
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            Debug.Log("jjj");
            player.TakeDamage(transform, enemySO.enemyDamageAmount);
        }
    }

    public void AttackColliderTurnOff()
    {
        polygonCollider2D.enabled = false;
    }

    public void AttackColliderTurnOn()
    {
        polygonCollider2D.enabled = true;
    }

}
