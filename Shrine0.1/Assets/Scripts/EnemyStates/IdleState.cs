using UnityEngine;
using UnityEditor;

class IdleState : IEnemyState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
        this.parent.MyTarget = null;
        this.parent.Reset();
    }

    public void Exit()
    {

    }

    public void Update()
    {
        //change into follow state if the player is within aggro range
        if (parent.MyTarget != null)
        {
            parent.ChangeState(new FollowState());
        }
    }
}