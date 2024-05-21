using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class NewBehaviourScript : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    
    private int _currentHealth;
    private bool _isAlive = true;

    private PlayerInput _input;
    private InputAction _move;
    //private Vector3 _oldInput;
    private Camera _camera;

    public int Health => _currentHealth;

    public EventHandler<int> TakeDamage => OnTakeDamage;
    public EventHandler<int> TakeHeal => OnHeal;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _move = _input.actions["Movement"];
    }

    private void Start()
    {
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

        Vector3 moveInput = _move.ReadValue<Vector2>();

        Vector3 _movementVector = _camera.transform.TransformDirection(moveInput);
        _movementVector.y = 0;
        _movementVector.Normalize();

        transform.forward = _movementVector;

        _characterController.Move(_movementVector * (_speed * Time.deltaTime));
        _animator.SetFloat("Speed", _characterController.velocity.magnitude);
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
