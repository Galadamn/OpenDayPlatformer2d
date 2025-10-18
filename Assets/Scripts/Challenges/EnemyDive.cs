using UnityEngine;

public class EnemyDive : MonoBehaviour
{
    public float moveSpeed;
    public float sightRange;
    [SerializeField] private float collisionRange;

    public LayerMask whatIsGround;
    
    private bool _canAttack;
    private Vector3 _attackDirection;
    private Transform _target;
    
    private Animator _animator;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        
        if (!Physics2D.OverlapCircle(transform.position, collisionRange, whatIsGround))
        {
            if (!_canAttack && Vector2.Distance(transform.position, _target.position) < sightRange)
            {
                _attackDirection = Vector3.Normalize(_target.position - transform.position);
                _canAttack = true;
            }
        }
        else
        {
            transform.localScale = new Vector2(1, -1);
            Destroy(gameObject, 0.5f);
            sightRange = 0;
            _canAttack = false;
        }
        
        if (_canAttack)
        {
            transform.position += _attackDirection * (moveSpeed * Time.deltaTime);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.chartreuse;
        Gizmos.DrawWireSphere(transform.position, sightRange);  //sight range gizmo
        Gizmos.color = Color.softRed;
        Gizmos.DrawWireSphere(transform.position, collisionRange);  //ground check gizmo
    }

    private void UpdateAnimation()
    {
        if (_canAttack)
        {
            _animator.Play("bat_fly");
        }
        else
        {
            _animator.Play("Bat_Idle");
        }
    }
    
    
    
    
    
}
