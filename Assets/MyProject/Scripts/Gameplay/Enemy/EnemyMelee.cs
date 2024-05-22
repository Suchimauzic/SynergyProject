using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Enemy, IMovable
{
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _speedIncrease = 0.1f;

    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;

    private float _currentSpeed = 0;

    public float Speed => _currentSpeed;
    public float MaxSpeed => _maxSpeed;
    public float SpeedIncrease => _speedIncrease;


    private void Awake()
    {
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new IdleState(_stateMachine, _animator));
        _stateMachine.AddState(new MoveState(_stateMachine, _animator, this));
        _stateMachine.SetState<IdleState>();

        _currentHealth = _maxHealth;
        _isAlive = _currentHealth > 0;
    }

    private void Update()
    {
        if (!_agent.hasPath)
        {
            _stateMachine.SetState<IdleState>();
            Vector3 point = RandomWayPoint();
            _agent.SetDestination(point);
        }
        else
        {
            _stateMachine.SetState<MoveState>();
        }

        _agent.speed = _animator.GetFloat("Speed");

        _stateMachine.Update();
    }

    private Vector3 RandomWayPoint()
    {
        NavMeshTriangulation data = NavMesh.CalculateTriangulation();
        int index = Random.Range(0, data.vertices.Length);
        return data.vertices[index];
    }
}
