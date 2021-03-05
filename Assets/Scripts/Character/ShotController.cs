using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public AudioClip throwSound;
    public GameObject projectile;
    public Transform firePointRight, firePointLeft;
    private CharController charContr;
    private AudioSource audio;
    private void Start() {
        charContr = GetComponent<CharController>();
        audio = GetComponent<AudioSource>();
    }
    void OnFire() {
        Debug.Log("fire");
        
        Transform currFirepoint = charContr.facingLeft ? firePointLeft : firePointRight;
        if (!charContr.isSliding)
            Fire(currFirepoint);    
    }

    

    void Fire(Transform firePoint) {
        audio.clip = throwSound;
        audio.Play();
        Instantiate(projectile, firePoint);
    }
}
