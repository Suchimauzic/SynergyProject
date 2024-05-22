using UnityEngine;

public class IdleState : StateSM
{
    public IdleState(StateMachine machine, Animator animator) : base(machine, animator) { }

    public override void Enter()
    {
        Debug.Log("[Enter] Idle");
        _animator.SetFloat("Speed", 0);
    }

    public override void Exit()
    {
        Debug.Log("[Exit] Idle");
    }

    public override void Update()
    {
        Debug.Log("[Update] Idle");
    }
}
