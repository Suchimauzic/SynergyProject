using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Attacker))]
public class NewBehaviourScript : MonoBehaviour, IDamageable
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    private Attacker _attacker;

    [Header("Player options")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _speedIncrease = 1f;
    private float _minSpeed = 1f;
    
    private int _currentHealth;
    private bool _isAlive = true;

    private float _currentSpeed = 1f;
    private PlayerInput _input;
    private InputAction _move;
    //private Vector3 _oldInput;
    private Camera _camera;

    #region Interfaces

    public int Health => _currentHealth;

    public EventHandler<int> TakeDamage => OnTakeDamage;
    public EventHandler<int> TakeHeal => OnHeal;

    #endregion

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _input = GetComponent<PlayerInput>();
        _move = _input.actions["Movement"];
    }

    private void Start()
    {
        _currentSpeed = _minSpeed;
        _currentHealth = _maxHealth;
        _isAlive = _currentHealth > 0;
        _camera = Camera.main;
    }

    private void Update()
    {
        // Old version of input system
        /*var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _oldInput = new Vector3(horizontal, 0, vertical);*/

        Vector3 moveInput = new Vector3(_move.ReadValue<Vector2>().x, 0, _move.ReadValue<Vector2>().y);

        Vector3 _movementVector = _camera.transform.TransformDirection(moveInput);
        _movementVector.y = 0;
        _movementVector.Normalize();

        transform.forward = _movementVector;

        if (moveInput.x != 0 || moveInput.z != 0)
        {
            if (_currentSpeed < _maxSpeed)
                _currentSpeed += _speedIncrease * Time.deltaTime;
        }
        else
        {
            _currentSpeed = _minSpeed;
        }

        _characterController.Move(_movementVector * (_currentSpeed * Time.deltaTime));
        _animator.SetFloat("Speed", _characterController.velocity.magnitude);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _attacker.MeleeAttack();
        }
    }

    public void Die()
    {
        print("Dead");
    }

    private void OnHeal(object sender, int heal)
    {
        if (_currentHealth < _maxHealth)
            _currentHealth += heal;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    private void OnTakeDamage(object sender, int damage)
    {
        if (sender is not Attacker)
            return;

        if (Health > damage)
        {
            _currentHealth -= damage;
        }
        else if (_isAlive)
        {
            _currentHealth = 0;
            Die();
        }

        print($"Health = {Health} | Damage = {damage}");
    }
}
