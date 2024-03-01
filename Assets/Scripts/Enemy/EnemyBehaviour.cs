using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Chasing")]
    [SerializeField] private float _radiusDetection;
    private bool _playerInRangeToChase;

    [Header("HUD")]
    [SerializeField] private Image _healthBar;

    [Header("Status")]
    [SerializeField] private float _health;
    private float _currentHealth;
    [SerializeField] private float _attackSpeed;
    private float _currentAttackSpeed;
    private bool _canAttack;

    [Header("Settings")]
    [SerializeField] private Transform _attackHitbox;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _playerLayer;

    private EnemyAnimation _animation;
    private NavMeshAgent _agent;
    private PlayerAnimation _player;

    public bool IsAlive { get => _currentHealth > 0; }

    private void Awake()
    {
        if (!TryGetComponent(out _agent))
            throw new Exception("O componente NavMeshAgent não foi associado a este obejto");

        if (!TryGetComponent(out _animation))
            throw new Exception("O script não foi associado a este obejto");

        _player = FindObjectOfType<PlayerAnimation>();
        if (_player == null)
            throw new Exception("O objeto não foi encontrado em cena");
    }

    // Start is called before the first frame update
    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _currentHealth = _health;
        _healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsAlive) return;

        var playerInAttackRange = Vector2.Distance(transform.position, _player.transform.position) <= _agent.stoppingDistance;

        if (playerInAttackRange)
        {
            if (_canAttack) Attack();
            else Idle();
        }
        else if (_playerInRangeToChase)
        {
            Move();
            ChangeDirection();
        }
        else
            Idle();

        if(!_canAttack)
            AttackCooldown();
        
        DetectPlayer();
    }

    private void Attack()
    {
        _animation.Attack();
    }

    private void AttackCooldown()
    {
        _currentAttackSpeed += Time.deltaTime;
        if (_currentAttackSpeed >= _attackSpeed)
        {
            _currentAttackSpeed = 0;
            _canAttack = true;
        }
    }

    /// <summary>
    /// Chamado durante a animação de ataque do inimigo como um evento
    /// </summary>
    public void OnAttack()
    {
        var hit = Physics2D.OverlapCircle(_attackHitbox.position, _radius, _playerLayer);
        if(hit != null)
        {
            _player.Hit();
        }
    }

    /// <summary>
    /// Chamado durante o último frame da animação de ataque do inimigo como um evento
    /// </summary>
    public void OnEndAttack()
        => _canAttack = false;

    private void Idle() =>
        _animation.Idle();

    private void Move()
    {
        _agent.SetDestination(_player.transform.position);
        _animation.Walk();
    }

    private void ChangeDirection()
    {
        var positionX = _player.transform.position.x - transform.position.x;
        transform.eulerAngles = new Vector2(0, positionX > 0 ? 0 : 180f);
    }

    public void Hit()
    {
        _currentHealth--;
        _healthBar.fillAmount = _currentHealth / _health;

        if (_currentHealth <= 0)
            Die();
        else
        _animation.Hit();
    }

    private void Die()
    {
        gameObject.layer = 0;
        _animation.Die();
    }

    /// <summary>
    /// Chamado durante o último frame da animãção de morte do inimigo como um evento
    /// </summary>
    public void OnDie()
    {
        Destroy(gameObject);
    }

    private void DetectPlayer()
    {
        var collider2D = Physics2D.OverlapCircle(transform.position, _radiusDetection, _playerLayer);
        _playerInRangeToChase = collider2D is not null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackHitbox.position, _radius);
        Gizmos.DrawWireSphere(transform.position, _radiusDetection);

    }
}
