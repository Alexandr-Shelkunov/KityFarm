using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{

    public static Player Instance { get; private set; }

    [SerializeField] private float movingSpeed = 10f;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float delayAfterDeath = 2f;
    [SerializeField] private float damageRecoveryTime;

    private Vector2 inputVector;

    private Rigidbody2D rb;

    public event EventHandler OnPlayerDeath;

    private bool isAlive;
    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;

    private int currentHealth;
    private bool canTakeDamage = true;

    private void Awake() 
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        currentHealth = maxHealth;
        isAlive = true;

        //HealthBar.Instance.SetMaxHealth(maxHealth);
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e) {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void Update() {
        inputVector = GameInput.Instance.GetMovementVector();
    }


    private void FixedUpdate() 
    {
        HandleMovement();
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void TakeDamage(Transform damageSourceTransform, int damage)
    {
        if (canTakeDamage && isAlive)
        {
            currentHealth = Math.Max(0, currentHealth -= damage);
            //HealthBar.Instance.SetHealth(currentHealth);

            //ScreenShakeManager.Instance.ShakeScreen();

            //if (damageSourceTransform)
            //{
            //    knockBack.GetKnockedBack(damageSourceTransform);
            //}

            //OnFlashBlink?.Invoke(this, EventArgs.Empty);
            canTakeDamage = false;
            StartCoroutine(DamageRecoveryRoutine());
        }

        DetectDeath();
    }

    private void HandleMovement() {
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));
        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed) {
            isRunning = true;
        } else {
            isRunning = false;
        }
    }

    public void HealPlayer(int healthAmount)
    {
        currentHealth = Math.Min(maxHealth, currentHealth += healthAmount);
        //HealthBar.Instance.SetHealth(currentHealth);
    }

    private void DetectDeath()
    {
        if (currentHealth == 0 && isAlive)
        {
            isAlive = false;
            //GameInput.Instance.DisableMovement();
            //knockBack.StopKnockBackMovement();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);

            StartCoroutine(DelayAfterDeathRoutine());
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private IEnumerator DelayAfterDeathRoutine()
    {
        yield return new WaitForSeconds(delayAfterDeath);
        //Loader.InstantLoad(Loader.Scene.GameOverScene);
    }

    public bool IsRunning() {
        return isRunning;
    }

    public Vector3 GetPlayerScreenPosition() {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

}
