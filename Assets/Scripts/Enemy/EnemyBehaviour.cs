using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Status")]
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
    }

    // Update is called once per frame
    private void Update()
    {
        var reachedPlayer = Vector2.Distance(transform.position, _player.transform.position) <= _agent.stoppingDistance;
        if (reachedPlayer && _canAttack)
            Attack();
        else if (reachedPlayer && !_canAttack)
            Idle();
        else
        {
            Move();
            ChanceDirection();
        }

        if(!_canAttack)
            AttackCooldown();
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

    private void ChanceDirection()
    {
        var positionX = _player.transform.position.x - transform.position.x;
        transform.eulerAngles = new Vector2(0, positionX > 0 ? 0 : 180f);
    }

    private void OnDrawGizmosSelected() =>
        Gizmos.DrawWireSphere(_attackHitbox.position, _radius);
}
