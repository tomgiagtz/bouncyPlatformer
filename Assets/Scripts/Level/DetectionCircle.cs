using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCircle: MonoBehaviour
{
    //assign enemy controller
    public EnemyController enemyController;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            //call on detect
            enemyController.OnDetect();
        }
    }
}
