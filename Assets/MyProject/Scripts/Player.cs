using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, IDamageable
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;

    private Vector3 _input;
    private Camera _camera;

    public int Health
    {
        get => _currentHealth;
        set { _currentHealth = value; }
    }

    //private Vector3 _movementVector;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _camera = Camera.main;
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _input = new Vector3(horizontal, 0, vertical);

        Vector3 _movementVector = _camera.transform.TransformDirection(_input);
        _movementVector.y = 0;
        _movementVector.Normalize();

        transform.forward = _movementVector;

        _characterController.Move(_movementVector * (_speed * Time.deltaTime));
        _animator.SetFloat("Speed", _characterController.velocity.magnitude);
    }

    #region IDamage
    
    public void Die()
    {
        
    }

    public void Heal(int heal)
    {

    }

    public void TakeDamage(int damage)
    {
        
    }

    #endregion
}
