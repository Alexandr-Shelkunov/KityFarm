using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    public event EventHandler OnAxeSwing;

    public void Attack()
    {
        OnAxeSwing?.Invoke(this, EventArgs.Empty);
    }

}
