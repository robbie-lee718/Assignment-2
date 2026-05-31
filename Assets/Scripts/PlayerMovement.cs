using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    float moveSpeed = 10f;
    float jumpForce = 9f;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public TextMeshProUGUI gameComplete;
    int maxJumps = 2;
    int jumpsRemaining;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxJumps;
        gameComplete.gameObject.SetActive(false);
    }

    int keyCount = 0;

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        float moveInput = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed){
            moveInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed){
            moveInput = 1f;
        }
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jumping
        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpsRemaining > 0){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsRemaining--;
        }

        // Flip sprite based on movement direction
        if (moveInput < 0){
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground")){
            jumpsRemaining = maxJumps;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Water")){
            RestartLevel();
        }
        if (other.CompareTag("Goal")){
            if (keyCount == 4){
                Debug.Log("Goal reached!");
                gameComplete.gameObject.SetActive(true);
            }
        }
        if (other.CompareTag("Enemy")){
            RestartLevel();
        }
        if (other.CompareTag("Key")){
            Destroy(other.gameObject);
            keyCount++;
        }
    }

    void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameComplete.gameObject.SetActive(false);
    }
}
