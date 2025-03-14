using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour 
{
    private const string IS_RUNNING = "IsRunning";
    private const string IS_DIE = "IsDie";

    private Animator animator;
    //private FlashBlink flashBlink;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //flashBlink = GetComponent<FlashBlink>();
    }

    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Update() 
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());

        if (Player.Instance.IsAlive())
        {
            AdjustPlayerFacingDirection();
        }
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
        //flashBlink.StopBlinking();
    }

    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        if (mousePos.x < playerPosition.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }
}
