using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed;
   
    public LayerMask whatIsWall;
    public LayerMask whatIsDanger;
    public Transform wallCheck;
    public Transform fallCheck;
    private Animator _animator;
    
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    private void FixedUpdate() 
    {
        _rigidbody2D.linearVelocityX = moveSpeed; //move the rigidbody at movespeed speed
    }
    
    private bool DetectedWallOrFall() 
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, whatIsWall) //check for walls infront of enemy
               || !Physics2D.OverlapCircle(fallCheck.position, 0.1f); //check for falls infront of enemy
    }

    /*   private bool DetectEnemy()
       {
           return Physics2D.OverlapCircle(wallCheck.position, 0.1f, whatIsDanger);
       }

       private void OnCollisionEnter2D(Collision2D other)
       {
           if (DetectEnemy())
           {
               moveSpeed *= -1;
               transform.localScale  = new Vector2(transform.localScale.x * -1f, 1f);
           }
       }
   */
    private void Update()
    {
        if (DetectedWallOrFall())
        { 
            moveSpeed *= -1; //flip the move direction
            transform.localScale  = new Vector2(transform.localScale.x * -1f, 1f); //flip the horizontal sprite direction
        }
        //UpdateAnimation();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(wallCheck.position, 0.1f);
        Gizmos.DrawWireSphere(fallCheck.position, 0.1f);
    }

    private void UpdateAnimation()
    {
        _animator.Play("Snake_Move");
    }
}
