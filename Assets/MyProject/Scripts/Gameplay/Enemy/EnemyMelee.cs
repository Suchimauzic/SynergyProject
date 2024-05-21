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

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _isAlive = _currentHealth > 0;
    }

    private void Update()
    {
        if (_agent.hasPath)
        {
            _currentSpeed += _speedIncrease * Time.deltaTime;
            
        }

        if (!_agent.hasPath)
        {
            _currentSpeed = 0;
            Vector3 point = RandomWayPoint();
            _agent.SetDestination(point);
        }

        _agent.speed = _currentSpeed;
        _animator.SetFloat("Speed", _currentSpeed);
    }

    private Vector3 RandomWayPoint()
    {
        NavMeshTriangulation data = NavMesh.CalculateTriangulation();
        int index = Random.Range(0, data.vertices.Length);
        return data.vertices[index];
    }
}
