using UnityEngine;
using UnityEditor;

class IdleState : IEnemyState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        //change into follow state if the player is within aggro range
        if (parent.Target != null)
        {
            parent.ChangeState(new FollowState());
        }
    }
}