using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCircle: MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyController enemyController;
    void Start()
    {
        
        Debug.Log(enemyController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            enemyController.OnDetect();
        }
    }
}
