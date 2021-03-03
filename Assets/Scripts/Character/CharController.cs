using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    CapsuleCollider2D col;
    public float movementX, velocityY;
    public float maxHorizSpeed = 20f;
    public float movementForce = 5f;
    public float jumpForce = 10f;
    public bool facingLeft, willJump;
    public bool isGrounded, isFrontTouching, isSliding;

    public LayerMask groundLayers, frontLayers;
    private Vector3 spawnPoint;

    Vector2 areaTopRight;
    Vector2 areaLowerLeft;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<CapsuleCollider2D>();
        spawnPoint = transform.position;

        standingColliderBounds = col.size;
        areaLowerLeft = Vector2.one;
        areaTopRight = Vector2.one;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        GroundCheck();
        FrontCheck();
        HandleFlip();
        SlideColliderUpdate();
        if (willJump) HandleJump();


        //movement force

        if (rb.velocity.x < maxHorizSpeed && !isSliding) {
            rb.AddForce(new Vector2(movementX, 0) * movementForce);
        }
        velocityY = rb.velocity.normalized.y;
        //counter force
        CounterMovement();
    }

    void GroundCheck() {
        //bottom left of the character sprite is the top right of the overlap area
        Vector2 bottomLeft = new Vector2(transform.position.x - 0.49f * sprite.bounds.size.x, transform.position.y - 0.5f * sprite.bounds.size.y);
        //get bottom right of character but subtract more on the y to get it below the feet
        Vector2 areaLowerRight = new Vector2(transform.position.x + 0.49f * sprite.bounds.size.x, transform.position.y - 0.58f * sprite.bounds.size.y);
        isGrounded = Physics2D.OverlapArea(bottomLeft, areaLowerRight, groundLayers);
    }
    
    void FrontCheck() {
        //bottom left of the character sprite is the top right of the overlap area
        areaTopRight = facingLeft ? 
        new Vector2(transform.position.x - 0.52f * sprite.bounds.size.x, transform.position.y + 0.5f * sprite.bounds.size.y) :
        new Vector2(transform.position.x + 0.52f * sprite.bounds.size.x, transform.position.y + 0.5f * sprite.bounds.size.y);
        //get bottom left of character but subtract more on the y to get it infront of the character
        areaLowerLeft = facingLeft ? 
        new Vector2(transform.position.x - 0.49f * sprite.bounds.size.x, transform.position.y - 0.4f * sprite.bounds.size.y) :
        new Vector2(transform.position.x + 0.49f * sprite.bounds.size.x, transform.position.y - 0.4f * sprite.bounds.size.y);
        isFrontTouching = Physics2D.OverlapArea(areaTopRight, areaLowerLeft, frontLayers);
    }


    float counterXVelocity;
    public float counterMovementFactor = 4f;
    void CounterMovement() {
        if (movementX == 0 && isGrounded && !isSliding) {
            counterXVelocity = -rb.velocity.x;
            rb.AddForce(new Vector2(counterXVelocity, 0.1f) * counterMovementFactor * movementForce);
        }
    }

    void HandleFlip() {
        sprite.flipX = facingLeft;
    }

    void HandleJump() {
        if (willJump) {
            willJump = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.normalized.x;
        // velocityY = movementVector.y;
        if (movementX == 0) {
            isSliding = false;
            return;
        }
        bool lastDirection = facingLeft;
        if (movementX > 0) {
            facingLeft = false;
        } else {
            facingLeft = true;
        }

        if (lastDirection != facingLeft) {
            isSliding = false;
        }
    }

    void OnJump() {
        if (isGrounded && !isSliding) {
        Debug.Log("jump");
            willJump = true;
        }
    }


    float minSlideSpeed;
    void OnSlide() {
        if (isGrounded && !isSliding) {
            isSliding = true;
        } else {
            isSliding = false;
        }
        Debug.Log("Sliding " + isSliding);
    }
    private Vector2 standingColliderBounds;
    public Vector2 slidingColliderBounds;
    void SlideColliderUpdate() {
        if (isSliding) {
            col.size = slidingColliderBounds;
        } else {
            col.size = standingColliderBounds;
        }
    }

    public void Respawn() {
        transform.position = spawnPoint;
        rb.velocity = Vector3.zero;
    }

    



    // private void OnDrawGizmos() {
    //     Vector3 gareaTopRight = new Vector3(areaTopRight.x, areaTopRight.y, transform.transform.position.z);
    //     Vector3 gareaLowerLeft = new Vector3(areaLowerLeft.x, areaLowerLeft.y, transform.transform.position.z);
    //     Debug.DrawLine(gareaTopRight, gareaLowerLeft);
    // }
}
