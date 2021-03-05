using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    public enum EnemyState {patrol, chase};
    public EnemyState curState = EnemyState.patrol;

    public Vector3 walkRunCurrSpeed;

    public Transform[] flipChecks;
    public float checkDistance = .2f;
    public float attackDistance = 1f;
    public LayerMask isGround;

    private bool isChasing;
    private bool facingRight = true;
    private bool canFlip = true;

    private GameObject target;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        if (curState == EnemyState.chase) {
            float direction = transform.position.x  - target.transform.position.x;
            if (direction > 0 && facingRight) {
                Flip();
            } else if (direction < 0 && !facingRight) {
                Flip();
            }

            if (Mathf.Abs(direction) < attackDistance) {
                animator.SetBool("isAttacking", true);
            } else {
                animator.SetBool("isAttacking", false);
            }

            walkRunCurrSpeed.z = Mathf.Lerp(walkRunCurrSpeed.z, walkRunCurrSpeed.y, Time.deltaTime);
        } else if (curState == EnemyState.patrol) {
            animator.SetBool("isAttacking", false);
            walkRunCurrSpeed.z = Mathf.Lerp(walkRunCurrSpeed.z, walkRunCurrSpeed.x, Time.deltaTime);
        }

        EnemyMove(walkRunCurrSpeed.z * Time.deltaTime);

    }

    private void FixedUpdate() {
        for (int i = 0; i < flipChecks.Length; i++) {
            Debug.DrawRay(flipChecks[i].position, Vector3.down * checkDistance, Color.red, .1f);
            if (!Physics2D.Raycast(flipChecks[i].position, Vector3.down, checkDistance, isGround) && canFlip) {
                Flip();
                StartCoroutine(setCanFlip());
            }
        }
    }

    private void Flip() {
        Debug.Log("flip");
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    void EnemyMove(float movement) {
        float dirMovement = facingRight ? movement : -1 * movement;
        Vector3 targetVel = new Vector2(dirMovement, rb.velocity.y);
        rb.velocity = targetVel;
    }

    public void OnDetect(GameObject _target) {
        curState = EnemyState.chase;
        target = _target;
    }

    public void OnEndDetect() {
        curState = EnemyState.patrol;
    }

    IEnumerator setCanFlip() {
        canFlip = false;    
        yield return new WaitForSeconds(.2f);
        canFlip = true;
    }
}
