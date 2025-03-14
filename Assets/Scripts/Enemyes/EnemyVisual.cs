using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{

    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private GameObject enemyShadow;
    [SerializeField] private EnemyAI enemyAI;

    private const string TAKEHIT = "TakeHit";
    private const string IS_DIE = "IsDie";
    private const string IS_RUNNING = "IsRunning";
    private const string CHASING_SPEED_MULTIPLIER = "ChasingSpeedMultiplier";
    private const string ATTACK = "Attack";

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemyEntity.OnDeath += EnemyEntity_OnDeath;
        enemyEntity.OnTakeHit += EnemyEntity_OnTakeHit;
        enemyAI.OnAttack += EnemyAI_OnAttack;
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, enemyAI.IsRunning());
        animator.SetFloat(CHASING_SPEED_MULTIPLIER, enemyAI.GetRoamingAnimationSpeed());
    } 

    private void EnemyAI_OnAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    private void EnemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TAKEHIT);
    }

    private void EnemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
        spriteRenderer.sortingOrder = -1;
        enemyShadow.SetActive(false);
    }
}
