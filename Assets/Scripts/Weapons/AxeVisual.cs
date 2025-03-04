using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AxeVisual : MonoBehaviour
{

    [SerializeField] private Axe axe;

    private Animator animator;
    private const string ATTACK = "Attack";


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
