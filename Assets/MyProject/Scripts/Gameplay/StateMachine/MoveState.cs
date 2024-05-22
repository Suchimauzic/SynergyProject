using UnityEngine;

public class MoveState : StateSM
{
    private IMovable _move;
    private float _speed;

    public MoveState(StateMachine machine, Animator animator, IMovable move) : base(machine, animator)
    {
        _move = move;
    }

    public override void Enter()
    {
        Debug.Log("[Enter] MoveState");
    }

    public override void Exit()
    {
        Debug.Log("[Exit] MoveState");
    }

    public override void Update()
    {
        if (_speed < _move.MaxSpeed)
            _speed += _move.SpeedIncrease * Time.deltaTime;

        _animator.SetFloat("Speed", _speed);
    }
}
