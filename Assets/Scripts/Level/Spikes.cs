using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<CharController>().Respawn();
        }
    }
}
