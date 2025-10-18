using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 7f;
    private float _lastInput = 1;

    public bool playerIsGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Vector2 groundBoxSize = new Vector2(0.8f, 0.2f);
    
    private InputManager _input;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Start()
    {
        _input = GetComponent<InputManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerIsGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, whatIsGround);
        if (_input.Jump && playerIsGrounded)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed;
        }

        /*if (!_input.JumpHeld)
        {
            _rigidbody2D.linearVelocityY = 0;
        }*/

        Attack();
        UpdateAnimation();
    }
    
    private void FixedUpdate()
    {
        if (_input.Horizontal != 0) _lastInput = _input.Horizontal;
        
        transform.localScale = new Vector2(_lastInput,1);
        _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
        if (_input.Horizontal != 0) transform.localScale = new Vector3(_input.Horizontal, 1, 1);
    }
    
    private void Attack()
    {
        if (!Physics2D.OverlapCircle(groundCheck.position, 0.2f, LayerMask.GetMask("Enemy")))return;

        var enemyColliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemyColliders)
        {
            Destroy(enemy.gameObject);
        }

        _rigidbody2D.linearVelocityY = jumpSpeed / 1.3f;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Victory"))
        {
            print("Victory");
        }
    }
    
    private void UpdateAnimation()
    {
        if (_input.Horizontal != 0) _lastInput = _input.Horizontal;
        
        transform.localScale = new Vector2(_lastInput,1);
        
        if (playerIsGrounded)
        {
            if (_input.Horizontal != 0)
            {
                _animator.Play("Player_walk");
            }
            else
            {
                _animator.Play("Player_idle");
            }
        }
        else
        { 
            if (_rigidbody2D.linearVelocityY > 0) // This checks our velocity and if it is above 0
            {
                _animator.Play("player_jump"); // If we are moving upwards, set animation to Jump
            }
            else // This checks our velocity and if it is below 0
            {
                _animator.Play("Player_fall"); // If we are moving downwards, set animation to Fall
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }
}
