using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSlashVisual : MonoBehaviour
{
    [SerializeField] private Axe axe;

    private string ATTACK = "Attack";
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        axe.OnAxeSwing += Axe_OnAxeSwing;
    }

    private void Axe_OnAxeSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

}
