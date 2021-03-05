using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            Debug.Log("Enemy");
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    
}
