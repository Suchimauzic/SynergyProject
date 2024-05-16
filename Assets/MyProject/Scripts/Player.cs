using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;

    private Vector3 _input;
    private Camera _camera;
    //private Vector3 _movementVector;

    private void Start()
    {
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
}
