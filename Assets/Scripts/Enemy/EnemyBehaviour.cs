using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform _attackHitbox;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _playerLayer;

    private EnemyAnimation _animation;
    private NavMeshAgent _agent;
    private Player _player;

    private void Awake()
    {
        if (!TryGetComponent(out _agent))
            throw new Exception("O componente NavMeshAgent não foi associado a este obejto");

        if (!TryGetComponent(out _animation))
            throw new Exception("O script não foi associado a este obejto");

        _player = FindObjectOfType<Player>();
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
        if (reachedPlayer) 
            Attack();
        else
        {
            Move();
            ChanceDirection();
        }
    }

    private void Attack()
    {
        _animation.Attack();
    }

    /// <summary>
    /// Chamado durante a animação de ataque do inimigo como um evento
    /// </summary>
    public void OnAttack()
    {
        var hit = Physics2D.OverlapCircle(_attackHitbox.position, _radius, _playerLayer);
        if(hit != null)
        {

        }
    }

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
