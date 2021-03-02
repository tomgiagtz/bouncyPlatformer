using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevelController : MonoBehaviour
{
    public GameObject finishLevelUI;
    public int nextLevelSceneID;
    public float waitTimer = 5f;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(loadNextLevel(nextLevelSceneID));
        }
    }

    // Update is called once per frame
    IEnumerator loadNextLevel(int nextLevel) {

        finishLevelUI.SetActive(true);
        yield return new WaitForSeconds(waitTimer);
        SceneManager.LoadScene(nextLevel);

    }
}
