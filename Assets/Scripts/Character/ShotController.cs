using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePointRight, firePointLeft;
    private CharController charContr;
    private void Start() {
        charContr = GetComponent<CharController>();
    }
    void OnFire() {
        Debug.Log("fire");
        
        Transform currFirepoint = charContr.facingLeft ? firePointLeft : firePointRight;
        if (!charContr.isSliding)
            Fire(currFirepoint);    
    }

    

    void Fire(Transform firePoint) {
        Instantiate(projectile, firePoint);
    }
}
