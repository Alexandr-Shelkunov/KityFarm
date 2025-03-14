using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour {

    public static ActiveWeapon Instance { get; private set; }

    [SerializeField] private Axe axe;

    private void Awake() {
        Instance = this;
    }


    public Axe GetActiveWeapon() {
        return axe;
    }

}
